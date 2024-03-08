using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp4.View;

namespace WpfApp4.Control
{

    internal class StartControl
    {
        private Mutex? mainAppMutex;
        private string[] Guids;
        private Start _appLimit;
        public void Max3()
        {
            bool createdNew;
            for (int i = 0; i < 3; i++)
            {
                mainAppMutex = new Mutex(true, Guids[i], out createdNew);
                if (!createdNew)
                {
                    if (i == 2)
                    {
                        MessageBox.Show("Max 3 App");
                        _appLimit.Close();
                        return;
                    }
                }
                else
                    break;
            }
        }

        public StartControl(Start limit)
        {
            _appLimit = limit;
            Guids = new string[3] {"C77B94B5-ED1B-4709-9C31-C3732ADFC613",
            "72798704-1AAE-4649-A428-21E4DCE3FEA6",
            "CA4CBC04-2898-43FF-9BA2-B0D5E43506FF" };
        }
    }
}
