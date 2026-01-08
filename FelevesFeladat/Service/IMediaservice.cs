using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.Service
{
    public interface IMediaService
    {
        Task<string> CapturePhotoAsync();
        Task<string> PickPhotoAsync();
    }
    internal class IMediaservice
    {
    }
}
