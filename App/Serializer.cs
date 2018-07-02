using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace App
{
    public class Serializer
    {
        public static CustomerInfoRequest GetCustomerInfoRequest(string xmlInputData)
        {
            CustomerInfoRequest obj = new CustomerInfoRequest();
            try
            {
                XDocument doc = XDocument.Parse(xmlInputData);
                IEnumerable<XElement> xElments = doc.Descendants("Image1");
                XElement customerInfoReqElm = xElments.First();
                obj.externalId = customerInfoReqElm.Element("externalId").Value;
                obj.image_id = customerInfoReqElm.Element("image_id").Value;
                obj.DateTime = customerInfoReqElm.Element("DateTime").Value;
                obj.ConfidenceScore = customerInfoReqElm.Element("ConfidenceScore").Value;
                obj.matchStatus = customerInfoReqElm.Element("matchStatus").Value;
                obj.capturedImage = InputOutput.ReadImageFromBase64String(customerInfoReqElm.Element("capturedImage").Value);
                obj.MatchedImage = InputOutput.ReadImageFromBase64String(customerInfoReqElm.Element("MatchedImage").Value);

                if (obj.matchStatus.Equals(Constants.VERIFIED))
                {
                    //var bitmap = InputOutput.BitmapImageToBitmap(obj.capturedImage);
                    //InputOutput.DrawBitmapWithGreenBorder(bitmap);
                    //obj.capturedImage = InputOutput.BitmapToBitmapImage(bitmap);
                }
            }
            catch
            {
                // Log error
                return null;
            }
            return obj;
        }
    }
}
