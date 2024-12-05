using AgroVision.Forms;
using AgroVision.Mapping;
using AgroVision.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace AgroVision
{
    public class DrawSupport
    {

        public event Action OnDrawEnd;

        private readonly Pen drawingPen;
        private readonly Pen outsidePen;

        private List<NPoint> tempPoints;
        private int hoverPointIndex = -1;
        private int selectedPointIndex = -1;
        private NPoint[] savedTempPoints;
        private readonly float pointSize = 16;
        private int mouseCursor = -1;
        private List<MapNote> notes;

        private int lastNoteIndex;
        private ToolTip tip;
        private readonly GoogleMapsView mapsView;
        public DrawSupport(GoogleMapsView mapsView)
        {
            lastNoteIndex = -1;
            tempPoints = new List<NPoint>();
            notes = new List<MapNote>();

            this.mapsView = mapsView;

            drawingPen = new Pen(Brushes.SkyBlue, 3f);
            outsidePen = new Pen(Brushes.LimeGreen, 3f);

            mapsView.OnDrawMap += OnDrawMap_mapsView;
            mapsView.MouseMove += OnMouseMove_mapsView;
            mapsView.MouseUp += OnMouseUp_mapsView;
            mapsView.OnKeyCmdPress += OnKeyCmdPress_mapsView;
        }

        #region Eventos
        private void OnDrawMap_mapsView(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            DrawOutsidePoints(e.Graphics);

            DrawNotes(e.Graphics);
        }

        private void DrawOutsidePoints(Graphics graphics)
        {
            List<PointF> drawedPolygonPoints = new List<PointF>();
            foreach (NPoint point in tempPoints)
                drawedPolygonPoints.Add(mapsView.CoordToPoint(point.ToCoord()).ToPoint());

            if (tempPoints.Count > 2)
            {
                graphics.FillPolygon(new SolidBrush(EnableDraw ? Color.FromArgb(50, 98, 219, 210) : Color.FromArgb(50, 0, 219, 210)), drawedPolygonPoints.ToArray());
                graphics.DrawPolygon(EnableDraw ? drawingPen : outsidePen, drawedPolygonPoints.ToArray());
            }

            if (EnableDraw)
                for (int tempPointIndex = 0; tempPointIndex < tempPoints.Count; tempPointIndex++)
                {
                    PointF p = mapsView.CoordToPoint(tempPoints[tempPointIndex].ToCoord()).ToPoint();
                    graphics.FillEllipse(tempPointIndex == selectedPointIndex ? Brushes.Yellow : Brushes.White, new RectangleF(p.X - (pointSize / 2), p.Y - (pointSize / 2), pointSize, pointSize));
                }
        }

        private void DrawNotes(Graphics graphics)
        {
            foreach (MapNote note in notes)
            {
                PointF point = mapsView.CoordToPoint(note.Point.ToCoord()).ToPoint();
                point.X -= 8;
                point.Y -= 20;
                RectangleF rect = new RectangleF(point, new Size(30, 30));

                note.Rectangle = rect;
                graphics.DrawImage(Properties.Resources.pin, rect);
            }
        }

        private void OnMouseMove_mapsView(object sender, MouseEventArgs e)
        {
            if (EnableDraw)
            {
                NPoint position = e.Location.ToNPoint();

                switch (e.Button)
                {
                    case MouseButtons.Left:
                        //Mover apenas um ponto
                        if (mouseCursor == 4)
                            if (hoverPointIndex != -1)
                            {
                                selectedPointIndex = hoverPointIndex;
                                tempPoints[hoverPointIndex] = mapsView.PointToCoord(position);
                            }

                        mapsView.Refresh();
                        break;
                    default:
                        mapsView.Cursor = Cursors.Cross;
                        hoverPointIndex = -1;
                        mouseCursor = -1;

                        if (tempPoints.Count > 2)
                        {
                            NPoint[] drawedTempPoints = new NPoint[tempPoints.Count];
                            savedTempPoints = new NPoint[tempPoints.Count];

                            for (int tempPointIndex = 0; tempPointIndex < tempPoints.Count; tempPointIndex++)
                            {
                                drawedTempPoints[tempPointIndex] = mapsView.CoordToPoint(new NPoint(tempPoints[tempPointIndex][0], tempPoints[tempPointIndex][1]).ToCoord());
                                savedTempPoints[tempPointIndex] = new NPoint(tempPoints[tempPointIndex][0], tempPoints[tempPointIndex][1]);
                            }
                        }

                        for (int tempPointIndex = 0; tempPointIndex < tempPoints.Count; tempPointIndex++)
                        {
                            NPoint point = mapsView.CoordToPoint(tempPoints[tempPointIndex].ToCoord());

                            float halfPoint = pointSize / 2;
                            if (position[0] > point[0] - halfPoint && position[0] < point[0] + halfPoint &&
                                position[1] > point[1] - halfPoint && position[1] < point[1] + halfPoint)
                            {
                                hoverPointIndex = tempPointIndex;

                                mapsView.Cursor = Cursors.Hand;
                                mouseCursor = 4;
                                break;
                            }
                        }
                        break;
                }
            }
            else
            {
                for (int noteIndex = 0; noteIndex < notes.Count; noteIndex++)
                {
                    MapNote note = notes[noteIndex];
                    if (note.Rectangle.Contains(e.Location) && lastNoteIndex != noteIndex)
                    {
                        if (tip != null)
                            tip.Dispose();

                        tip = new ToolTip
                        {
                            IsBalloon = true
                        };

                        Point point = e.Location;
                        point.X -= 15;
                        point.Y -= 45;

                        tip.Show(note.Content, mapsView, point);

                        lastNoteIndex = noteIndex;
                        break;
                    }
                    else if (!note.Rectangle.Contains(e.Location) && lastNoteIndex == noteIndex)
                    {
                        lastNoteIndex = -1;

                        if (tip != null)
                            tip.Dispose();
                    }
                }
            }
        }

        private void OnMouseUp_mapsView(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    if (EnableDraw)
                    {
                        if (mouseCursor == -1)
                            tempPoints.Add(mapsView.PointToCoord(e.Location.ToNPoint()));

                        mapsView.Refresh();
                    }
                    break;
                case MouseButtons.Right:
                    MapNote clickedNote = null;
                    for (int noteIndex = 0; noteIndex < notes.Count; noteIndex++)
                    {
                        MapNote note = notes[noteIndex];
                        if (note.Rectangle.Contains(e.Location))
                            clickedNote = note;
                    }

                    ContextMenu context = new ContextMenu(new[] {
                                new MenuItem($"{ (clickedNote != null ? "Editar" : "Adicionar") } Nota", delegate
                                {
                                    NoteForm noteForm = new NoteForm();
                                    noteForm.NoteDeleted += delegate
                                    {
                                        notes.Remove(clickedNote);

                                        mapsView.Refresh();
                                    };

                                    if(clickedNote != null)
                                        noteForm.Content = clickedNote.Content;

                                    if(noteForm.ShowDialog() == DialogResult.OK){
                                        if(clickedNote != null)
                                            clickedNote.Content = noteForm.Content;
                                        else
                                            notes.Add(new MapNote(noteForm.Content, mapsView.PointToCoord(new NPoint((double)e.X, (double)e.Y))));

                                        mapsView.Refresh();
                                    }
                                })
                            });

                    context.Show(mapsView, e.Location);
                    break;
            }
        }

        private void OnKeyCmdPress_mapsView(object sender, Keys e)
        {
            switch (e)
            {
                case Keys.Enter:
                    EndDraw(mapsView);
                    break;
                case Keys.Escape:
                    mapsView.Refresh();

                    EndDraw(mapsView);
                    break;
                case Keys.Delete:
                    DeletePoint();
                    break;
            }
        }
        #endregion

        public void DeletePoint()
        {
            if (selectedPointIndex >= 0 && selectedPointIndex < tempPoints.Count)
            {
                tempPoints.RemoveAt(selectedPointIndex);

                if (tempPoints.Count == 0)
                    mapsView.Cursor = Cursors.Default;

                mapsView.Refresh();
            }
        }

        public Polygon GetPolygon()
        {
            if (tempPoints.Count > 2)
                return new Polygon(tempPoints.ToArray());
            else
                return new Polygon(new NPoint[0]);
        }

        public void SetPolygon(List<NPoint> points)
        {
            tempPoints = points;

            mapsView.Refresh();
        }

        public List<MapNote> GetNotes()
        {
            return notes;
        }

        public void SetNotes(List<MapNote> notes)
        {
            this.notes = notes;

            mapsView.Refresh();
        }

        public void EndDraw(GoogleMapsView mapsView)
        {
            if (OnDrawEnd != null)
            {
                OnDrawEnd();

                mapsView.Cursor = Cursors.Default;

                EnableDraw = false;

                mapsView.Refresh();
            }
        }

        public bool EnableDraw { get; set; }

        public List<MapNote> Notes
        {
            get { return notes; }
            set { notes = value; mapsView.Refresh(); }
        }

        public void DisposeSupport()
        {
            mapsView.OnDrawMap -= OnDrawMap_mapsView;
            mapsView.MouseMove -= OnMouseMove_mapsView;
            mapsView.MouseUp -= OnMouseUp_mapsView;

            tempPoints.Clear();

            mapsView.Refresh();
        }

    }
}