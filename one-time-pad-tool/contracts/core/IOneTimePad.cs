using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace one_time_pad_tool.contracts.core
{
    interface IOneTimePad
    {
        void EncryptFile(string file_path, string out_path, string pad_path, bool base64Pad = false);
        void DecryptFileBase64Pad(string file_path, string out_path, string pad_path);
        void DecryptFile(string file_path, string out_path, string pad_path);
    }
}
