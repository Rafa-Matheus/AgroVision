using AgroVision.CustomViews;
using AgroVision.Mapping;
using AgroVision.Mapping.Distances;
using AgroVision.Rastering;
using AgroVision.Utils;
using AgroVision.Views;
using DHS.Imaging;
using DHS.IO;
using DHS.Tasking;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace AgroVision.Forms
{
    public partial class NewMission : UserControl
    {

        #region Propriedades
        public bool FileIsSaved { get; set; }
        #endregion

        #region Variáveis
        private readonly SpinnerView cultura;
        private readonly SwitchView obstaculoCima;
        private readonly SwitchView obstaculoBaixo;
        private readonly SwitchView obstaculoColisao;
        private readonly SwitchView obstaculoLados;
        private readonly SwitchView obstaculoPouso;
        private readonly SwitchView obstaculoApas;
        private readonly SwitchView obstaculoRth;
        private readonly SpinnerView modo;
        private readonly SwitchView rth;
        private readonly SeekBarView altura;
        private readonly SeekBarView angulo;
        private readonly SeekBarView margem;
        private readonly SeekBarView velocidade;
        private readonly SpinnerView acaoFinal;
        private readonly SpinnerView direcao;

        //Câmera
        private readonly SwitchView tirarFotos;
        private readonly SeekBarView cameraIntervalo;

        //Gimbal
        private readonly SeekBarView gimbalX;
        private readonly SeekBarView gimbalY;
        private readonly SeekBarView gimbalZ;

        private readonly SeekBarView sobreposicaoHorizontal;
        private readonly SeekBarView sobreposicaoVertical;
        #endregion

        #region Inicializar
        private int unitIndex;
        private readonly AgrarianMesure[] units;

        private readonly Haversine haversine;

        private readonly BoxInfoView painelAvancado;
        public NewMission()
        {
            InitializeComponent();

            //
            nameTbx.TextChanged += delegate
            {
                FileIsSaved = false;
            };

            units = new[] {
                new AgrarianMesure(1, "m²", ""),
                new AgrarianMesure(4047, "Acre", "s"),
                new AgrarianMesure(10000, "Hect.", ""),
                new AgrarianMesure(24200, "Alq.", "")
            };

            LoadDroneModels();
            MenuItem[] droneModelContextMenuItems = DroneModels.Select(m => DroneModelToContextMenuItem(m)).ToArray();
            droneInfoBx.Description = "(Nenhum drone selecionado)";
            droneInfoBx.OnPressActionButton += delegate
            {
                ContextMenu context = new ContextMenu(droneModelContextMenuItems);

                context.Show(droneInfoBx, droneInfoBx.PointToClient(Cursor.Position));
            };

            etaBx.Description = "00:00:00s";
            reqBatBx.Description = "1";
            gsdBx.Description = "0,0 cm/px";

            LoadCameras();
            MenuItem[] cameraContextMenuItems = Cameras.Select(c => CameraToContextMenuItem(c)).ToArray();
            gsdBx.OnPressActionButton += delegate
            {
                List<MenuItem> cameras = new List<MenuItem>
                {
                    CameraToContextMenuItem(null)
                };
                cameras.AddRange(cameraContextMenuItems);

                ContextMenu context = new ContextMenu(cameras.ToArray());

                context.Show(gsdBx, gsdBx.PointToClient(Cursor.Position));
            };

            unitIndex = (int)Properties.Settings.Default["metrics_unit"];

            areaBx.Description = "0 m²";
            areaBx.SideButtonsVisible = true;
            areaBx.LeftClick += delegate
            {
                if (unitIndex - 1 > -1)
                {
                    unitIndex--;
                    Properties.Settings.Default["metrics_unit"] = unitIndex;
                    UpdateInfo();
                }
            };
            areaBx.RightClick += delegate
            {
                if (unitIndex + 1 < units.Length)
                {
                    unitIndex++;
                    Properties.Settings.Default["metrics_unit"] = unitIndex;
                    UpdateInfo();
                }
            };

            //
            cultura = new SpinnerView();
            cultura.OnValueChange += delegate
            {
                FileIsSaved = false;
            };
            cultura.AddOption(new SpinnerOption("Cana-de-açúcar", "cana"));
            cultura.AddOption(new SpinnerOption("Milho", "milho"));
            cultura.AddOption(new SpinnerOption("Soja", "soja"));
            cultura.AddOption(new SpinnerOption("Café", "cafe"));
            cultura.AddOption(new SpinnerOption("Citricultura", "citricultura"));
            ControlUtil.AddCustomField(new CustomControl("Cultura", "", Properties.Resources.cultura, cultura, settings));

            rth = new SwitchView
            {
                Value = true
            };

            obstaculoCima = new SwitchView
            {
                Value = true
            };
            obstaculoBaixo = new SwitchView
            {
                Value = true
            };
            obstaculoColisao = new SwitchView
            {
                Value = true
            };
            obstaculoLados = new SwitchView
            {
                Value = true
            };
            obstaculoPouso = new SwitchView
            {
                Value = true
            };
            obstaculoApas = new SwitchView
            {
                Value = true
            };
            obstaculoRth = new SwitchView
            {
                Value = true
            };

            modo = new SpinnerView();
            modo.OnValueChange += delegate
            {
                FileIsSaved = false;
            };
            modo.AddOption(new SpinnerOption("Normal", "NORMAL"));
            modo.AddOption(new SpinnerOption("Encurvado", "CURVED"));
            modo.Value = "CURVED";

            altura = new SeekBarView
            {
                MinValue = 1,
                MaxValue = 500,
                Value = 120,
                Unit = "m"
            };
            altura.OnValueChange += delegate
            {
                UpdateInfo();

                RefreshMap();
            };

            ControlUtil.AddCustomField(new CustomControl("Altura", "", Properties.Resources.altura, altura, settings));

            angulo = new SeekBarView
            {
                MinValue = 0,
                MaxValue = 360,
                Unit = "°"
            };
            angulo.OnValueChange += delegate
            {
                UpdateInfo();

                RefreshMap();
            };
            ControlUtil.AddCustomField(new CustomControl("Ângulo", "", Properties.Resources.angulo, angulo, settings));

            margem = new SeekBarView
            {
                MinValue = -30,
                MaxValue = 30,
                Unit = "m"
            };
            margem.OnValueChange += delegate
            {
                UpdateInfo();

                RefreshMap();
            };
            ControlUtil.AddCustomField(new CustomControl("Margem", "", Properties.Resources.margem, margem, settings));

            velocidade = new SeekBarView
            {
                MinValue = 3,
                MaxValue = 72,
                Unit = "m/s"
            };
            velocidade.OnValueChange += delegate
            {
                UpdateInfo();
            };
            ControlUtil.AddCustomField(new CustomControl("Velocidade", "", Properties.Resources.velocidade, velocidade, settings));

            //
            acaoFinal = new SpinnerView();
            acaoFinal.OnValueChange += delegate
            {
                FileIsSaved = false;
            };
            acaoFinal.AddOption(new SpinnerOption("Passar para o controle manual", "NO_ACTION"));
            acaoFinal.AddOption(new SpinnerOption("Voltar para o ponto inicial (I)", "GO_FIRST_WAYPOINT"));
            acaoFinal.AddOption(new SpinnerOption("Pousar", "AUTO_LAND"));
            acaoFinal.AddOption(new SpinnerOption("Ficar parado no último ponto", "CONTINUE_UNTIL_END"));
            acaoFinal.AddOption(new SpinnerOption("Voltar e pousar no Home Point", "GO_HOME"));

            //
            direcao = new SpinnerView();
            direcao.OnValueChange += delegate
            {
                FileIsSaved = false;
            };
            direcao.AddOption(new SpinnerOption("Automático", "AUTO"));
            direcao.AddOption(new SpinnerOption("Apontar para o próximo ponto", "USING_WAYPOINT_HEADING"));
            direcao.AddOption(new SpinnerOption("Controlar pelo rádio", "CONTROL_BY_REMOTE_CONTROLLER"));
            direcao.AddOption(new SpinnerOption("Posição da decolagem", "USING_INITIAL_DIRECTION"));
            //direcao.AddOption(new SpinnerOption("Ponto de interesse", "TOWARD_POINT_OF_INTEREST"));

            gimbalX = new SeekBarView
            {
                MinValue = -90,
                MaxValue = 90,
                Value = 0,
                Unit = "°"
            };
            gimbalX.OnValueChange += delegate
            {
                FileIsSaved = false;
            };
            gimbalY = new SeekBarView
            {
                MinValue = -90,
                MaxValue = 90,
                Unit = "°"
            };
            gimbalY.OnValueChange += delegate
            {
                FileIsSaved = false;
            };
            gimbalZ = new SeekBarView
            {
                MinValue = -90,
                MaxValue = 90,
                Unit = "°"
            };
            gimbalZ.OnValueChange += delegate
            {
                FileIsSaved = false;
            };

            tirarFotos = new SwitchView
            {
                Value = false
            };
            tirarFotos.OnValueChange += delegate
            {
                FileIsSaved = false;
            };

            cameraIntervalo = new SeekBarView
            {
                MinValue = 1,
                MaxValue = 15,
                Unit = "s"
            };
            cameraIntervalo.OnValueChange += delegate
             {
                 FileIsSaved = false;
             };

            //
            sobreposicaoHorizontal = new SeekBarView();
            sobreposicaoHorizontal.OnValueChange += delegate
            {
                UpdateInfo();

                RefreshMap();
            };
            sobreposicaoHorizontal.MinValue = 0;
            sobreposicaoHorizontal.MaxValue = 100;
            sobreposicaoHorizontal.Unit = "%";

            sobreposicaoVertical = new SeekBarView();
            sobreposicaoVertical.OnValueChange += delegate
            {
                UpdateInfo();

                RefreshMap();
            };
            sobreposicaoVertical.MinValue = 0;
            sobreposicaoVertical.MaxValue = 100;
            sobreposicaoVertical.Unit = "%";

            CustomControl[] avancado = new CustomControl[] {
                new CustomControl("Voltar para casa se desconectar", "", Properties.Resources.rth, rth, null),
                new CustomControl("Sensor de Cima", "", Properties.Resources.obstaculo, obstaculoCima, null),
                new CustomControl("Sensor de Baixo", "", Properties.Resources.obstaculo, obstaculoBaixo, null),
                new CustomControl("Sensor de Proa e Popa", "", Properties.Resources.obstaculo, obstaculoColisao, null),
                new CustomControl("Sensor dos Lados", "", Properties.Resources.obstaculo, obstaculoLados, null),
                new CustomControl("Sensor de Proteção do Pouso", "", Properties.Resources.obstaculo, obstaculoPouso, null),
                new CustomControl("Sensor de Desvio Ativo (APAS)", "", Properties.Resources.obstaculo, obstaculoApas, null),
                new CustomControl("Sensor do Retorno Inteligente", "", Properties.Resources.obstaculo, obstaculoRth, null),
                new CustomControl("Modo do Caminho", "Normal: O drone fará trocas de caminho em ângulos vivos\nEncurvado: O drone fará curvas suaves a cada retomada sem parar", Properties.Resources.modo, modo, null),
                new CustomControl("Ação ao Finalizar", "", Properties.Resources.final, acaoFinal, null),
                new CustomControl("Direção da Proa", "", Properties.Resources.head, direcao, null),

                //Camera
                new CustomControl("Ativar Câmera", "", Properties.Resources.tirar_fotos, tirarFotos, null),
                new CustomControl("Intervalo da Câmera", "", Properties.Resources.camera_intervalo, cameraIntervalo, null),

                //Gimbal
                new CustomControl("Inclinação do Gimbal (Pitch)", "", Properties.Resources.gimbal_x, gimbalX, null),
                //new CustomControl("Rotação do Gimbal (Yaw)", "", Properties.Resources.gimbal_y, gimbal_y,null),
                //new CustomControl("Rolagem do Gimbal (Roll)", "", Properties.Resources.gimbal_z, gimbal_z,null),

                new CustomControl("Sobreposição Horizontal", "", Properties.Resources.sobreposicao, sobreposicaoHorizontal, null),
                new CustomControl("Sobreposição Vertical", "", Properties.Resources.sobreposicao, sobreposicaoVertical, null)
            };
            painelAvancado = ControlUtil.AddSession("Avançado", Properties.Resources.advanced, settings, avancado);

            haversine = new Haversine();
            maxBatteryMinutes = 25;
            cameraImageWidth = 0;
        }

        private List<JObject> DroneModels { get; set; }

        private void LoadDroneModels()
        {
            DroneModels = new List<JObject>();

            string[] models = Directory.GetFiles(@".\models");
            for (int modelIndex = 0; modelIndex < models.Length; modelIndex++)
            {
                string modelFile = models[modelIndex];
                if (modelFile.EndsWith(".json"))
                {
                    using (StreamReader file = File.OpenText(modelFile))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject model = (JObject)JToken.ReadFrom(reader);

                        DroneModels.Add(model);
                    }
                }
            }
        }

        private MenuItem DroneModelToContextMenuItem(JObject model)
        {
            string name = $"{model["manufacturer"]} {model["model"]}";

            return new MenuItem(name, delegate
            {
                if (!string.IsNullOrWhiteSpace(droneInfoBx.Description))
                {
                    if (MessageBox.Show("Se mudar o modelo, mudará as configurações dos sensores. Deseja continuar?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Warning).Equals(DialogResult.Yes))
                        ChangeDroneModel(name, model, true);
                }
                else
                    ChangeDroneModel(name, model, true);
            });
        }

        private JObject lastSelectedDroneModel;
        private void ChangeDroneModel(string name, JObject model, bool overwrite)
        {
            lastSelectedDroneModel = model;

            maxBatteryMinutes = model["maxBatteryMinutes"].ToObject<int>();
            cameraImageWidth = model["camera"]["width"].ToObject<int>();
            cameraSensorImageWidth = model["camera"]["sensorWidthInMm"].ToObject<double>();
            cameraFocalLengthInMm = model["camera"]["focalLengthInMm"].ToObject<double>();

            obstaculoCima.Parent.Parent.Visible = model["sensors"]["upSensor"]["exists"].ToObject<bool>();
            obstaculoBaixo.Parent.Parent.Visible = model["sensors"]["downSensor"]["exists"].ToObject<bool>();
            obstaculoColisao.Parent.Parent.Visible = model["sensors"]["collisionSensor"]["exists"].ToObject<bool>();
            obstaculoLados.Parent.Parent.Visible = model["sensors"]["sideSensor"]["exists"].ToObject<bool>();
            obstaculoPouso.Parent.Parent.Visible = model["sensors"]["landingSensor"]["exists"].ToObject<bool>();
            obstaculoApas.Parent.Parent.Visible = model["sensors"]["apasSensor"]["exists"].ToObject<bool>();
            obstaculoRth.Parent.Parent.Visible = model["sensors"]["rthSensor"]["exists"].ToObject<bool>();

            if (overwrite)
            {
                obstaculoCima.Value = model["sensors"]["upSensor"]["enabled"].ToObject<bool>();
                obstaculoBaixo.Value = model["sensors"]["downSensor"]["enabled"].ToObject<bool>();
                obstaculoColisao.Value = model["sensors"]["collisionSensor"]["enabled"].ToObject<bool>();
                obstaculoLados.Value = model["sensors"]["sideSensor"]["enabled"].ToObject<bool>();
                obstaculoPouso.Value = model["sensors"]["landingSensor"]["enabled"].ToObject<bool>();
                obstaculoApas.Value = model["sensors"]["apasSensor"]["enabled"].ToObject<bool>();
                obstaculoRth.Value = model["sensors"]["rthSensor"]["enabled"].ToObject<bool>();
            }

            droneInfoBx.Description = name;

            UpdateInfo();
        }

        private List<JObject> Cameras { get; set; }

        private void LoadCameras()
        {
            Cameras = new List<JObject>();

            string[] cameras = Directory.GetFiles(@".\cameras");
            for (int cameraIndex = 0; cameraIndex < cameras.Length; cameraIndex++)
            {
                string cameraFile = cameras[cameraIndex];
                if (cameraFile.EndsWith(".json"))
                {
                    using (StreamReader file = File.OpenText(cameraFile))
                    using (JsonTextReader reader = new JsonTextReader(file))
                    {
                        JObject camera = (JObject)JToken.ReadFrom(reader);

                        Cameras.Add(camera);
                    }
                }
            }
        }

        private MenuItem CameraToContextMenuItem(JObject camera)
        {
            if (camera == null)
            {
                return new MenuItem("Do próprio drone", delegate
                {
                    if (lastSelectedDroneModel != null)
                    {
                        cameraImageWidth = lastSelectedDroneModel["camera"]["width"].ToObject<int>();
                        cameraSensorImageWidth = lastSelectedDroneModel["camera"]["sensorWidthInMm"].ToObject<double>();
                        cameraFocalLengthInMm = lastSelectedDroneModel["camera"]["focalLengthInMm"].ToObject<double>();

                        lastSelectedCameraName = null;

                        UpdateInfo();
                    }
                    else
                        MessageBox.Show("Nenhum drone selecionado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                });
            }

            string name = $"{camera["manufacturer"]} {camera["model"]}";

            return new MenuItem(name, delegate
            {
                ChangeCamera(name, camera);
            });
        }

        private string lastSelectedCameraName;
        private void ChangeCamera(string name, JObject camera)
        {
            lastSelectedCameraName = name;

            cameraImageWidth = camera["width"].ToObject<int>();
            cameraSensorImageWidth = camera["sensorWidthInMm"].ToObject<double>();
            cameraFocalLengthInMm = camera["focalLengthInMm"].ToObject<double>();

            UpdateInfo();
        }
        #endregion

        #region Eventos
        private int lastButtonIndex;
        private void OnDrawMap_zigZagTracer(object sender, PaintEventArgs e)
        {
            if (!mapsView.Map.LockMove)
            {
                ZigZagTracer tracer = new ZigZagTracer();

                //float overlap = 5 * (sobreposicao_vertical.Value / 100f);
                //5 + overlap
                float flyHeigth = altura.Value / 2;
                plain = tracer.TracePaths(mapsView.Map, flyHeigth, angulo.Value, margem.Value, support.GetPolygon(), e.Graphics);
            }
        }

        private void OnDrawMap(object sender, PaintEventArgs e)
        {
            //Botões
            for (int buttonIndex = 0; buttonIndex < buttons.Count; buttonIndex++)
            {
                RectangleF rect = new RectangleF(10 + (buttonIndex * 58), 10, 48, 48);
                CircleMissionButton btn = buttons[buttonIndex];
                btn.Rectangle = rect;
                e.Graphics.DrawImage(btn.Icon, rect);
            }
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                for (int buttonIndex = 0; buttonIndex < buttons.Count; buttonIndex++)
                {
                    CircleMissionButton circleBtn = buttons[buttonIndex];
                    if (circleBtn.Rectangle.Contains(e.Location))
                    {
                        circleBtn.PerformClick();
                        break;
                    }
                }
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            for (int buttonIndex = 0; buttonIndex < buttons.Count; buttonIndex++)
            {
                CircleMissionButton circleBtn = buttons[buttonIndex];
                if (circleBtn.Rectangle.Contains(e.Location) && lastButtonIndex != buttonIndex)
                {
                    if (tip != null)
                        tip.Dispose();

                    tip = new ToolTip();

                    Point point = e.Location;
                    point.X += 20;
                    point.Y += 20;
                    tip.Show(circleBtn.Title, mapsView, point);

                    lastButtonIndex = buttonIndex;
                    break;
                }
                else if (!circleBtn.Rectangle.Contains(e.Location) && lastButtonIndex == buttonIndex)
                {
                    lastButtonIndex = -1;

                    tip.Dispose();
                }
            }
        }
        #endregion

        #region Métodos
        private void RefreshMap()
        {
            if (mapsView != null)
                mapsView.Refresh();
        }

        public void SetMissionName(string name)
        {
            nameTbx.Text = name;
        }

        private string mode;
        private List<CircleMissionButton> buttons;
        private string equation;
        private MapView mapsView;
        private DrawSupport support;
        private RasterBatch rasterBatch;
        private PathPlanning plain;
        private ToolTip tip;
        private MetricsHelper metricsHelper;
        private DateTime date;
        public void SetMap(string mode, MapView view)
        {
            lastButtonIndex = -1;

            buttons = new List<CircleMissionButton>
            {
                new CircleMissionButton("Salvar", delegate
                {
                    SaveMission();
                    SaveImages();

                    NotificationManager.Clear();
                    NotificationManager.Success("Missão Salva", "A missão foi salva com sucesso");
                })
                { Icon = Properties.Resources.salvar }
            };

            this.mode = mode;
            switch (mode)
            {
                case "DRAW":
                    DrawMode();
                    break;
                case "MOUNT":
                    MountMode();
                    break;
            }

            mapsView = view;

            support = new DrawSupport(mapsView.Map);
            support.OnDrawEnd += delegate
            {
                mapsView.Map.LockMove = false;

                mapsView.Refresh();

                SetPolygonAndCompareDistance();

                UpdateInfo();

                NotificationManager.Clear();
                NotificationManager.Info("Quase pronto!", "Verifique os parâmetros da missão no painel à esquerda");
            };

            mapsView.Map.OnDrawMap += OnDrawMap_zigZagTracer;

            rasterBatch = new RasterBatch();
            rasterBatch.RegisterEvents(mapsView.Map);

            mapsView.Map.OnDrawMap += OnDrawMap;
            mapsView.Map.MouseUp += OnMouseUp;
            mapsView.Map.MouseMove += OnMouseMove;

            mapsView.Refresh();

            metricsHelper = new MetricsHelper();
            metricsHelper.Ranges.Add(new RangeValue("Sem Vegetação/Morta", -1, 0));
            metricsHelper.Ranges.Add(new RangeValue("Precisa de Intervenção", 0, .33));
            metricsHelper.Ranges.Add(new RangeValue("Bom", .33, .66));
            metricsHelper.Ranges.Add(new RangeValue("Muito Bom", .66, 1));

            rasterBatch.SettingUpImages += delegate
            {
                OpeningImagesMessage();
            };
            rasterBatch.RenderingStart += delegate
            {
                NotificationManager.Clear();
                NotificationManager.Info("Carregando imagens", "Aguarde...");
            };
            rasterBatch.RenderCompleted += delegate
            {
                NotificationManager.Clear();
                NotificationManager.Info("Imagens carregadas", "Defina o índice pela calculadora");// e a escala de cores");

                FileIsSaved = false;

                mapsView.Refresh();
            };
        }

        private void DrawMode()
        {
            AddDrawButton();

            HidePanelsForDrawMode();
        }

        private void AddDrawButton()
        {
            buttons.Add(new CircleMissionButton("Desenhar", delegate
            {
                if (mapsView != null)
                {
                    mapsView.Map.LockMove = support.EnableDraw = !mapsView.Map.LockMove;

                    if (!mapsView.Map.LockMove)
                        support.EndDraw(mapsView.Map);

                    mapsView.Refresh();
                }
            })
            { Icon = Properties.Resources.desenhar });
        }

        private void HidePanelsForDrawMode()
        {
            sobreposicaoHorizontal.Parent.Parent.Visible =
            sobreposicaoVertical.Parent.Parent.Visible = false;
        }

        private void MountMode()
        {
            AddMountButton();
            AddCalculatorButton();
            //AddColorsButton();
            AddMetricsButton();
            AddManageImagesButton();

            HidePanelsForMountMode();
        }

        private void AddMountButton()
        {
            buttons.Add(new CircleMissionButton("Montar Mapa", delegate
            {
                MountMapDialog mount = new MountMapDialog();
                mount.TiffButton += delegate
                {
                    OpenFileDialog openDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        Filter = "Arquivo de Imagem Tagueada|*.tif;*.tiff;*.jpg"
                    };
                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (mapsView != null)
                        {
                            string[] images = openDialog.FileNames.ToArray();
                            int imagesCount = images.Length;
                            if (imagesCount > 750)
                            {
                                MessageBox.Show("O número máximo é de 750 imagens por mapeamento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                imagesCount = 750;
                            }

                            AsyncWorker worker = new AsyncWorker();
                            worker.DoWork += delegate
                            {
                                //Limpar imagens
                                rasterBatch.Clear();

                                //Carregar imagens
                                for (int imageIndex = 0; imageIndex < imagesCount; imageIndex++)
                                    rasterBatch.AddImage(images[imageIndex]);
                            };
                            worker.OnWorkerCompleted += delegate
                            {
                                if (images.Length > 0)
                                {
                                    GeoTag geo = rasterBatch.Images[0].GPSPosition;
                                    date = geo.Date;
                                    mapsView.Map.Goto(geo.Latitude, geo.Longitude);

                                    //Comparar com o ponto da imagem
                                    imagePoint = new NPoint(geo.Latitude, geo.Longitude);

                                    SetPolygonAndCompareDistance();

                                    //Girar as imagens automaticamente
                                    if (plain != null)
                                        for (int imageIndex = 0; imageIndex < rasterBatch.Images.Count; imageIndex++)
                                        {
                                            RasterImage img = rasterBatch.Images[imageIndex];

                                            img.Rotation = ZigZagTracer.GetRotationFactor(mapsView.Map, plain, img.GPSPosition);
                                        }
                                }

                                ColorBlender blender = new ColorBlender();

                                blender.AddColor(Color.Maroon, 0);
                                blender.AddColor(Color.Red, .5f);
                                blender.AddColor(Color.Yellow, .6f);
                                //blender.AddColor(Color.LimeGreen, .75f);
                                blender.AddColor(Color.Green, 1);
                                blender.Blend();

                                rasterBatch.Blender = blender;

                                rasterBatch.UpdateEquation(null, delegate
                                {
                                    rasterBatch.UpdateColors(null);
                                });
                            };
                            worker.Start();
                        }
                    }
                };
                mount.FileButton += delegate
                {
                    OpenFileDialog openDialog = new OpenFileDialog
                    {
                        Multiselect = true,
                        Filter = "Mapeamento do AgroVision|*.agvm"
                    };
                    if (openDialog.ShowDialog() == DialogResult.OK)
                    {
                        if (mapsView != null)
                        {
                            RasterData data = Binary.ReadFromBinary<RasterData>(openDialog.FileName);

                            AsyncWorker worker = new AsyncWorker();
                            worker.DoWork += delegate
                            {
                                //Limpar imagens
                                rasterBatch.Clear();

                                List<RasterImageData> images = data.Images;
                                for (int imageIndex = 0; imageIndex < images.Count; imageIndex++)
                                    rasterBatch.AddBatchImage(images[imageIndex]);
                            };
                            worker.OnWorkerCompleted += delegate
                            {
                                if (rasterBatch.Images.Count > 0)
                                {
                                    GeoTag geo = rasterBatch.Images[0].GPSPosition;
                                    date = geo.Date;
                                    mapsView.Map.Goto(geo.Latitude, geo.Longitude);

                                    //Comparar com o ponto da imagem
                                    imagePoint = new NPoint(geo.Latitude, geo.Longitude);

                                    SetPolygonAndCompareDistance();
                                }

                                support.SetNotes(data.Notes.ToList());

                                equation = data.Equation;
                                rasterBatch.Blender = data.Blender;
                                rasterBatch.Thumbnail = data.Thumbnail;

                                rasterBatch.UpdateEquation(equation, delegate
                                {
                                    rasterBatch.UpdateColors(null);
                                });
                            };

                            worker.Start();
                        }
                    }
                };
                mount.ShowDialog();
            })
            { Icon = Properties.Resources.montar_mapa });
        }

        private void AddCalculatorButton()
        {
            buttons.Add(new CircleMissionButton("Calculadora", delegate
            {
                RasterCalculator calc = new RasterCalculator();
                if (equation != null)
                    calc.Equation = equation;

                if (calc.ShowDialog() == DialogResult.OK)
                {
                    equation = calc.Equation;

                    rasterBatch.UpdateEquation(equation, delegate
                    {
                        rasterBatch.UpdateColors(delegate
                        {
                            rasterBatch.CreateThumb();
                        });
                    });
                }
            })
            { Icon = Properties.Resources.calculadora });
        }

        //private void AddColorsButton()
        //{
        //    buttons.Add(new CircleMissionButton("Cores", delegate
        //    {
        //        FalseColors colors = new FalseColors();
        //        if (rasterBatch.Blender != null)
        //        {
        //            colors.Positions = rasterBatch.Blender.Positions.ToList();
        //            colors.Colors = rasterBatch.Blender.Colors.ToList();

        //            colors.UpdateColors();
        //        }
        //        if (colors.ShowDialog() == DialogResult.OK)
        //        {
        //            ColorBlender blender = new ColorBlender();
        //            for (int colorIndex = 0; colorIndex < colors.Positions.Count; colorIndex++)
        //                blender.AddColor(colors.Colors[colorIndex], colors.Positions[colorIndex]);

        //            blender.Blend();

        //            rasterBatch.Blender = blender;
        //            rasterBatch.UpdateColors(delegate
        //            {
        //                rasterBatch.CreateThumb();
        //            });
        //        }
        //    })
        //    { Icon = Properties.Resources.cores });
        //}

        private void AddMetricsButton()
        {
            buttons.Add(new CircleMissionButton("Métricas", delegate
            {
                MetricsForm metrics = new MetricsForm();
                if (rasterBatch.Blender != null)
                {
                    metrics.SetColors(rasterBatch.Blender);

                    metricsHelper.Count(rasterBatch.Thumbnail, rasterBatch.Blender);

                    metrics.AddFromMetricsHelper(metricsHelper, rasterBatch.Thumbnail, date);
                }
                metrics.ShowDialog();
            })
            { Icon = Properties.Resources.metricas });
        }

        private void AddManageImagesButton()
        {
            buttons.Add(new CircleMissionButton("Imagens", delegate
            {
                ManageImagesForm manageImages = new ManageImagesForm
                {
                    Batch = rasterBatch
                };
                manageImages.OnViewUpdated += delegate
                {
                    rasterBatch.CreateThumb();
                };
                manageImages.Show();
            })
            { Icon = Properties.Resources.imagens });
        }

        private void HidePanelsForMountMode()
        {
            rth.Parent.Parent.Visible =
            obstaculoCima.Parent.Parent.Visible =
            obstaculoBaixo.Parent.Parent.Visible =
            obstaculoColisao.Parent.Parent.Visible =
            obstaculoLados.Parent.Parent.Visible =
            obstaculoPouso.Parent.Parent.Visible =
            obstaculoApas.Parent.Parent.Visible =
            obstaculoRth.Parent.Parent.Visible =

            tirarFotos.Parent.Parent.Visible =
            cameraIntervalo.Parent.Parent.Visible =

            gimbalX.Parent.Parent.Visible =
            //gimbalY.Parent.Parent.Visible =
            //gimbalZ.Parent.Parent.Visible =

            acaoFinal.Parent.Parent.Visible =
            direcao.Parent.Parent.Visible =
            velocidade.Parent.Parent.Visible =
            droneInfoBx.Enabled =
            etaBx.Visible =
            reqBatBx.Visible = false;

            droneInfoBx.ActionIcon = null;

            //Travar itens
            cultura.Parent.Parent.Enabled =
            altura.Parent.Parent.Enabled =
            angulo.Parent.Parent.Enabled =
            margem.Parent.Parent.Enabled = false;

            //Redimensionar
            infoFlp.Height = 180;
            settings.Top = 260;
            settings.Height = 460;
            topPnl.Height = 310;

            painelAvancado.Visible = false;
        }

        private void OpeningImagesMessage()
        {
            if (InvokeRequired)
                Invoke(new MethodInvoker(() => OpeningImagesMessage()));
            else
            {
                NotificationManager.Clear();
                NotificationManager.Info("Abrindo imagens", "Aguarde...");
            }
        }

        private int maxBatteryMinutes;
        private int cameraImageWidth;
        private double cameraSensorImageWidth;
        private double cameraFocalLengthInMm;
        public void UpdateInfo()
        {
            if (cameraImageWidth > 0)
                gsdBx.Description = $"{MetricsUtil.GetGSD(cameraImageWidth, cameraSensorImageWidth, cameraFocalLengthInMm, altura.Value):N2} cm/pixel";
            else
                gsdBx.Description = "?";

            if (mapsView != null)
            {
                if (plain != null)
                {
                    double distanceInMeters = MetricsUtil.GetTotalDistance(plain, haversine);

                    int eta = MetricsUtil.GetEstimatedTimeArrival(distanceInMeters, velocidade.Value);
                    etaBx.Description = MetricsUtil.FormatSecondsToTimeString(eta);

                    reqBatBx.Description = MetricsUtil.GetRequiredBatteriesCount(eta, 60 * maxBatteryMinutes).ToString();

                    int totalPoints = 0;
                    for (int pointIndex = 0; pointIndex < plain.Paths.Count; pointIndex++)
                        totalPoints += plain.Paths[pointIndex].Points.Count;

                    NotificationManager.Clear();
                    if (totalPoints > 99)
                        NotificationManager.Error("Limite de Pontos", "Não é possível criar mais de 99 pontos");
                }

                if (support != null)
                {
                    Polygon polygon = support.GetPolygon();
                    if (polygon.Points.Length > 0)
                    {
                        double area = MetricsUtil.GetArea(polygon.Points);
                        AgrarianMesure measure = units[unitIndex];
                        string desc = measure.Format(area);

                        areaBx.Description = desc;
                    }
                }

                if (rasterBatch != null)
                {
                    rasterBatch.FlyHeight = altura.Value;
                    rasterBatch.Angle = angulo.Value;
                }
            }

            FileIsSaved = false;
        }
        #endregion

        #region Abrir/Fechar
        private string lastFile;
        public void OpenMission(string filePath)
        {
            lastFile = filePath;

            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);

            XmlNode root = doc.SelectSingleNode("//mission");

            nameTbx.Text = root.Attributes["name"].InnerText;
            string modelName = root.Attributes["model"].InnerText;
            droneInfoBx.Description = modelName;

            bool modelFounded = false;
            for (int droneModelIndex = 0; droneModelIndex < DroneModels.Count; droneModelIndex++)
            {
                JObject model = DroneModels[droneModelIndex];
                string name = $"{model["manufacturer"]} {model["model"]}";
                if (name.Equals(modelName))
                {
                    ChangeDroneModel(name, model, false);
                    modelFounded = true;
                    break;
                }
            }

            if (!modelFounded)
                MessageBox.Show($"Desculpe, o drone com o nome de '{modelName}' não foi encontrado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            if (root.Attributes["camera"] != null)
                if (!string.IsNullOrWhiteSpace(root.Attributes["camera"].InnerText))
                {
                    string cameraName = root.Attributes["camera"].InnerText;

                    bool cameraFounded = false;
                    for (int cameraIndex = 0; cameraIndex < Cameras.Count; cameraIndex++)
                    {
                        JObject camera = Cameras[cameraIndex];
                        string name = $"{camera["manufacturer"]} {camera["model"]}";
                        if (name.Equals(cameraName))
                        {
                            ChangeCamera(name, camera);
                            cameraFounded = true;
                            break;
                        }
                    }

                    if (!cameraFounded)
                        MessageBox.Show($"Desculpe, a câmera com o nome de '{cameraName}' não foi encontrada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

            for (int propertyIndex = 0; propertyIndex < root.ChildNodes.Count; propertyIndex++)
            {
                XmlNode node = root.ChildNodes[propertyIndex];
                string value = node.InnerText;
                switch (node.Name)
                {
                    case "option":
                        switch (node.Attributes["name"].InnerText)
                        {
                            case "obstacle_avoidance_top":
                                obstaculoCima.Value = bool.Parse(value);
                                break;
                            case "obstacle_avoidance_bottom":
                                obstaculoBaixo.Value = bool.Parse(value);
                                break;
                            case "obstacle_avoidance_collision":
                                obstaculoColisao.Value = bool.Parse(value);
                                break;
                            case "obstacle_avoidance_side":
                                obstaculoLados.Value = bool.Parse(value);
                                break;
                            case "obstacle_avoidance_landing":
                                obstaculoPouso.Value = bool.Parse(value);
                                break;
                            case "obstacle_avoidance_apas":
                                obstaculoLados.Value = bool.Parse(value);
                                break;
                            case "obstacle_avoidance_rth":
                                obstaculoLados.Value = bool.Parse(value);
                                break;
                            case "rth_on_rc_disconnect":
                                rth.Value = bool.Parse(value);
                                break;
                            case "culture":
                                cultura.Value = value;
                                break;
                            case "heading_mode":
                                direcao.Value = value;
                                break;
                            case "flight_height":
                                altura.Value = int.Parse(value);
                                break;
                            case "flight_angle":
                                angulo.Value = int.Parse(value);
                                break;
                            case "flight_offset":
                                margem.Value = int.Parse(value);
                                break;
                            case "h_overlap":
                                sobreposicaoHorizontal.Value = int.Parse(value);
                                break;
                            case "v_overlap":
                                sobreposicaoVertical.Value = int.Parse(value);
                                break;
                            case "auto_flight_speed":
                                velocidade.Value = int.Parse(value);
                                break;
                            case "flight_path_mode":
                                modo.Value = value;
                                break;
                            case "finished_action":
                                acaoFinal.Value = value;
                                break;
                            case "take_photo":
                                tirarFotos.Value = bool.Parse(value);
                                break;
                            case "camera_interval":
                                cameraIntervalo.Value = int.Parse(value);
                                break;
                            case "gimbal_pitch":
                                gimbalX.Value = int.Parse(value);
                                break;
                            case "gimbal_yaw":
                                gimbalY.Value = int.Parse(value);
                                break;
                            case "gimbal_roll":
                                gimbalZ.Value = int.Parse(value);
                                break;
                        }
                        break;
                    case "polygon":
                        double[] values = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();
                        List<NPoint> points = new List<NPoint>();
                        for (int pointIndex = 0; pointIndex < values.Length - 1; pointIndex += 2)
                        {
                            double xValue = values[pointIndex];
                            double yValue = values[pointIndex + 1];

                            points.Add(new NPoint(xValue, yValue));
                        }

                        //Definir posição
                        if (mapsView != null)
                        {
                            NPoint point = points[0];
                            mapsView.Map.Goto(point[0], point[1]);
                        }

                        support.SetPolygon(points);

                        SetPolygonAndCompareDistance();
                        break;
                    case "note":
                        double[] coord = node.Attributes["coord"].InnerText.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => double.Parse(p)).ToArray();

                        support.Notes.Add(new MapNote(value, new NPoint(coord[0], coord[1])));

                        mapsView.Refresh();
                        break;
                }
            }

            UpdateInfo();

            NotificationManager.Clear();

            if (mode == "DRAW")
                NotificationManager.Info("Desenhar Área", "Clique no lápis para começar, depois aperte Enter");
            else
                NotificationManager.Info("Montar Mapa", "Abra as imagens pelo quebra-cabeça, depois defina a equação do índice");// e cores");
        }

        private NPoint imagePoint;
        private void SetPolygonAndCompareDistance()
        {
            if (rasterBatch != null)
            {
                Polygon polygon = support.GetPolygon();

                rasterBatch.Polygon = polygon;

                //Emitir aviso
                if (imagePoint != null)
                    if (polygon.Points.Length > 0)
                        if (haversine.Calculate(polygon.Points[0], imagePoint) > 100)
                        {
                            MessageBox.Show("Atenção, você está trabalhando com imagem(s) que não fazem parte do plano de voo original.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            rasterBatch.EnableClip = false;

                            mapsView.Refresh();
                        }
            }
        }

        public void SaveMission()
        {
            //Apagar arquivo para não ficar dois
            if (lastFile != null)
                File.Delete(lastFile);

            DJIMissionBuilder builder = new DJIMissionBuilder(nameTbx.Text, Main.CURRENT_USER, droneInfoBx.Description, lastSelectedCameraName, support);

            //Assistência
            if (obstaculoCima.Parent.Parent.Visible)
                builder.Options.Add(new DJIMissionOption("obstacle_avoidance_top", $"{obstaculoCima.Value}".ToLower()));

            if (obstaculoBaixo.Parent.Parent.Visible)
                builder.Options.Add(new DJIMissionOption("obstacle_avoidance_bottom", $"{obstaculoBaixo.Value}".ToLower()));

            if (obstaculoColisao.Parent.Parent.Visible)
                builder.Options.Add(new DJIMissionOption("obstacle_avoidance_collision", $"{obstaculoColisao.Value}".ToLower()));

            if (obstaculoLados.Parent.Parent.Visible)
                builder.Options.Add(new DJIMissionOption("obstacle_avoidance_side", $"{obstaculoLados.Value}".ToLower()));

            if (obstaculoPouso.Parent.Parent.Visible)
                builder.Options.Add(new DJIMissionOption("obstacle_avoidance_landing", $"{obstaculoPouso.Value}".ToLower()));

            if (obstaculoApas.Parent.Parent.Visible)
                builder.Options.Add(new DJIMissionOption("obstacle_avoidance_apas", $"{obstaculoApas.Value}".ToLower()));

            if (obstaculoRth.Parent.Parent.Visible)
                builder.Options.Add(new DJIMissionOption("obstacle_avoidance_rth", $"{obstaculoRth.Value}".ToLower()));

            builder.Options.Add(new DJIMissionOption("rth_on_rc_disconnect", $"{rth.Value}".ToLower()));

            //Cultura
            builder.Options.Add(new DJIMissionOption("culture", $"{cultura.Value}".ToLower()));

            //Voo
            builder.Options.Add(new DJIMissionOption("goto_first_waypoint_mode", "SAFELY"));
            builder.Options.Add(new DJIMissionOption("heading_mode", $"{direcao.Value}"));
            builder.Options.Add(new DJIMissionOption("flight_height", $"{altura.Value}"));
            builder.Options.Add(new DJIMissionOption("flight_angle", $"{angulo.Value}"));
            builder.Options.Add(new DJIMissionOption("flight_offset", $"{margem.Value}"));
            builder.Options.Add(new DJIMissionOption("h_overlap", $"{sobreposicaoHorizontal.Value}"));
            builder.Options.Add(new DJIMissionOption("v_overlap", $"{sobreposicaoVertical.Value}"));
            builder.Options.Add(new DJIMissionOption("max_flight_height", "50"));
            builder.Options.Add(new DJIMissionOption("auto_flight_speed", $"{velocidade.Value}"));
            builder.Options.Add(new DJIMissionOption("max_flight_speed", $"{velocidade.Value}"));
            builder.Options.Add(new DJIMissionOption("flight_path_mode", $"{modo.Value}"));
            builder.Options.Add(new DJIMissionOption("finished_action", $"{acaoFinal.Value}"));
            builder.Options.Add(new DJIMissionOption("repeat_times", "1"));

            //Câmera
            builder.Options.Add(new DJIMissionOption("take_photo", $"{tirarFotos.Value}".ToLower()));
            builder.Options.Add(new DJIMissionOption("camera_interval", $"{cameraIntervalo.Value}"));

            //Gimbal
            if (gimbalX.Value != 0)
                builder.Options.Add(new DJIMissionOption("gimbal_pitch", $"{gimbalX.Value}"));

            if (gimbalY.Value != 0)
                builder.Options.Add(new DJIMissionOption("gimbal_yaw", $"{gimbalY.Value}"));

            if (gimbalZ.Value != 0)
                builder.Options.Add(new DJIMissionOption("gimbal_roll", $"{gimbalZ.Value}"));

            //Pontos
            if (plain != null)
                for (int pathIndex = 0; pathIndex < plain.Paths.Count; pathIndex++)
                {
                    MissionPath zigzagPath = plain.Paths[pathIndex];
                    for (int pointIndex = 0; pointIndex < zigzagPath.Points.Count; pointIndex++)
                        builder.Points.Add(zigzagPath.Points[pointIndex]);
                }

            string path = @".\users\";
            string userPath = $"{path}{Main.CURRENT_USER}\\";
            if (!Directory.Exists(userPath))
                Directory.CreateDirectory(userPath);

            builder.SaveFile($"{userPath}\\{nameTbx.Text}.agv");

            FileIsSaved = true;
        }

        public void SaveImages()
        {
            if (rasterBatch != null)
                if (rasterBatch.Images != null)
                    if (rasterBatch.Images.Count > 0)
                    {
                        RasterData data = new RasterData
                        {
                            Date = date,
                            Notes = support.Notes,
                            Blender = rasterBatch.Blender,
                            Equation = equation,
                            Thumbnail = rasterBatch.Thumbnail,
                            Min = -1,
                            Max = 1,
                            Images = rasterBatch.Images.Select(p => new RasterImageData(p.ImagePath, p.Values, p.Rotation, p.IsVisible)).ToList()
                        };

                        Binary.WriteToBinary(data, $"{Path.GetDirectoryName(lastFile)}\\mapeamento_{date.Day}_{date.Month}_{date.Year}.agvm");
                    }
        }

        public void Exit()
        {
            NotificationManager.Clear();

            //Limpar tudo
            rasterBatch.Clear();
            rasterBatch.UnregisterEvents();

            mapsView.Map.OnDrawMap -= OnDrawMap_zigZagTracer;
            mapsView.Map.OnDrawMap -= OnDrawMap;
            mapsView.Map.MouseUp -= OnMouseUp;
            mapsView.Map.MouseMove -= OnMouseMove;

            if (tip != null)
                tip.Dispose();

            Dispose();
            support.DisposeSupport();
        }
        #endregion

    }
}