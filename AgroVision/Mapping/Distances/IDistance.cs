using AgroVision.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroVision.Mapping.Distances
{
    public interface IDistance
    {

        double Calculate(params NPoint[] points);

    }
}
