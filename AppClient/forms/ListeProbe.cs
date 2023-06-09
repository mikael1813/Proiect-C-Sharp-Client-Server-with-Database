﻿using AppModel;
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
    partial class ListeProbe : Form
    {
        AppClientCtrl ctrl;
        Inscriere inscriere;
        List<Proba> listProbe;
        public ListeProbe(AppClientCtrl ctrl)
        {
            InitializeComponent();
            this.ctrl = ctrl;
            dataGridViewProbe.DataSource = ctrl.getProbe();
            this.inscriere = new Inscriere(ctrl);
            ctrl.updateEvent += userUpdate;
            //ctrl.updateEvent += userUpdate;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void probaSelected(object sender, DataGridViewCellEventArgs e)
        {

            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewProbe.Rows[e.RowIndex];
                float dist = float.Parse(row.Cells[0].Value.ToString());
                Stil stil = (Stil)Enum.Parse(typeof(Stil), row.Cells[1].Value.ToString());
                int id = int.Parse(row.Cells[2].Value.ToString());
                Proba p = new Proba(dist, stil);
                p.id = id;
                //listProbe = ctrl.getParticipantiDupaProba(p);
                dataGridViewParticipanti.DataSource = ctrl.getParticipantiDupaProba(p);
            }

        }

        private void participantSelected(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridViewParticipanti.Rows[e.RowIndex];
                int varsta = int.Parse(row.Cells[0].Value.ToString());
                string nume = row.Cells[1].Value.ToString();
                int id = int.Parse(row.Cells[2].Value.ToString());
                Participant p = new Participant(varsta, nume);
                p.id = id;

                dataGridViewProbeParticipant.DataSource = ctrl.getProbeDupaParticipanti(p);
            }


        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewParticipanti_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void logout(object sender, EventArgs e)
        {
            this.Hide();
            ctrl.logout();
            Login login = new Login(ctrl);
            login.ShowDialog();
            this.Close();
        }

        private void ListeProbe_Load(object sender, EventArgs e)
        {

        }

        public void userUpdate(object sender, AppUserEventArgs e)
        {
            if (e.UserEventType == AppUserEvent.NewInscriere)
            {
                Update update = new Update(ctrl);
                Console.WriteLine("DDDDDDDDDDDDDDDDDDDDD");
                update.ShowDialog();
                //dataGridViewProbe.DataSource = ctrl.getProbe();
            }
            if(e.UserEventType == AppUserEvent.update)
            {
                //IEnumerable<Proba> list = (IEnumerable<Proba>)e.Data;
                //dataGridViewProbe = null;
                //dataGridViewProbe.DataSource = list;
                // Daca vrei sa faci asa trebuie sa deschizi o lista de probe noua;cum ai chef
                //dataGridViewParticipanti.BeginInvoke(new UpdateListBoxCallback(this.updateListBox), new Object[] { dataGridViewParticipanti, list });
                Console.WriteLine("aaaaaaaaaaaaaaaaaa");
            }
        }
        //for updating the GUI

        //1. define a method for updating the ListBox
        private void updateListBox(ListBox listBox, IList<String> newData)
        {
            listBox.DataSource = null;
            listBox.DataSource = newData;
        }

        //2. define a delegate to be called back by the GUI Thread
        public delegate void UpdateListBoxCallback(ListBox list, IList<String> data);

        private void inscreire(object sender, EventArgs e)
        {
            inscriere.ShowDialog();
            //ctrl.updateEvent += userUpdate;
            //ctrl.updateEvent += userUpdate;
        }

        private void Close(object sender, FormClosingEventArgs e)
        {
            Console.WriteLine("ChatWindow closing " + e.CloseReason);
            if (e.CloseReason == CloseReason.UserClosing)
            {
                ctrl.logout();
                ctrl.updateEvent -= userUpdate;
                //Application.Exit();
            }
        }
    }
}
