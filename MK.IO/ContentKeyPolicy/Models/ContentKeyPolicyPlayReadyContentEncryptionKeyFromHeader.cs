using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MK.IO
{
    public class ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader : PlayReadyContentKeyLocation
    {
       public ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader()
        {
        }
           
        internal string OdataType => "#Microsoft.Media.ContentKeyPolicyPlayReadyContentEncryptionKeyFromHeader";
    }
}
