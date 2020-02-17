using System;
using System.Collections.Generic;
using System.Text;

namespace one_time_pad_tool.contracts.core.helpers
{
    interface IFileHelper
    {
        void SecureDelete(string file_path);
        void ConvertFileToBase64(string pad_path);
        bool CheckFileBase64(string file_path);
    }
}
