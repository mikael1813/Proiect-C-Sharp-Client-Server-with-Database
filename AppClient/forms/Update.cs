using AppModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppClient.forms
{
    partial class Update : Form
    {
        AppClientCtrl ctrl;
        public Update(AppClientCtrl ctrl)
        {
            this.ctrl = ctrl;
            InitializeComponent();
            List<Proba> list = (List<Proba>)ctrl.getProbe();
            List<ProbacuParticipant> list2 = new List<ProbacuParticipant>();
            List<int> list3 = ctrl.getNrParticipanti();
            for (int i = 0; i < list.Count; i++)
            {
                ProbacuParticipant p = new ProbacuParticipant(list[i], list3[i]);
                list2.Add(p);
            }
            dataGridView1.DataSource = list2;
            foreach (ProbacuParticipant p in list2)
            {
                Console.WriteLine(p);
            }
        }

        private void Update_Load(object sender, EventArgs e)
        {

        }
    }
}
