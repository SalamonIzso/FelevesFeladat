using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.Service
{
    public interface IMapService
    {
        Task<string> GetMapUrlAsync(MenuItem review);
    }
    public class IMapservice
    {
    }
}
