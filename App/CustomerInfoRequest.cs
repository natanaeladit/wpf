using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

namespace App
{
    public class CustomerInfoRequest
    {
        public string externalId { get; set; }
        public string image_id { get; set; }
        public string DateTime { get; set; }
        public string ConfidenceScore { get; set; }
        public string matchStatus { get; set; }
        public BitmapImage capturedImage { get; set; }
        public BitmapImage MatchedImage { get; set; }
    }
}
