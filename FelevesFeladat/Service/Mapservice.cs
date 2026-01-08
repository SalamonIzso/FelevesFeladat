using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelevesFeladat.Service
{
    public class MapService : IMapService
    {
        public async Task<string> GetMapUrlAsync(MenuItem review)
        {
            // Alapértelmezett értékek (pl. Budapest), ha nincs mentett helyszín
            double lat = 47.4979;
            double lon = 19.0402;
            bool hasLocation = false;

            if (review != null && review.Latitude != 0 && review.Longitude != 0)
            {
                lat = review.Latitude;
                lon = review.Longitude;
                hasLocation = true;
            }

            var culture = System.Globalization.CultureInfo.InvariantCulture;
            double zoom = 0.02;
            string bbox = $"{(lon - zoom).ToString(culture)}%2C{(lat - zoom).ToString(culture)}%2C{(lon + zoom).ToString(culture)}%2C{(lat + zoom).ToString(culture)}";

            string url = $"https://www.openstreetmap.org/export/embed.html?bbox={bbox}&layer=mapnik";

            // Csak akkor rakunk rá piros tüzet (markert), ha valóban van koordinátánk
            if (hasLocation)
            {
                url += $"&marker={lat.ToString(culture)}%2C{lon.ToString(culture)}";
            }

            return url;
        }
    }
}
