using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RabantFinanceManager.Security
{
    public class CustomerLinkEncryption
    {
        //we use the class DataProtectorTokenProvider which has a property called Protector of IDataProtector
        // an Interface which is used for protector and unprotector with the method GenerateAsync
        //which is passed to the protect method and also ValidateAsync method which is passed the unprotect
        //method for decryption
        //-----------------------------------------------------------------------------------------------
        public readonly string CustomerLinkEncryptionString = "CustomerLinkEncryptionString";
    }
}
