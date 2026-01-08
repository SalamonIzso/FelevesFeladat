using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.Service
{
    public class GeocodingService : IGeocodingService
    {
        public async Task<Location> GetLocationAsync(string address)
        {
            try
            {
                var locations = await Geocoding.Default.GetLocationsAsync(address);
                return locations?.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
