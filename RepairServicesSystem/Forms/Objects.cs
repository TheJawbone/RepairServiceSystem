﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System_obsługi_napraw.Modes;
using BusinessLayer;
namespace RepairServicesSystem
{
    public partial class Objects : Form
    {
        private Mode mode;
        public int nr_obj { get; set; }

        public Objects(Mode mode)
        {
            InitializeComponent();
            if(mode == Mode.WORKER)
            {
                ButtonAddObject.Enabled = false;
                ButtonEditObject.Enabled = false;
                ViewBtn.Enabled = false;
                AddReqBtn.Enabled = false;
            }
            dataGridView1.DataSource = ObjectFacade.GetObjectsDataTable();
            this.mode = mode;
        }

        private void ButtonSearchClient_Click(object sender, EventArgs e)
        {
            var form = new Users(mode);
            form.ShowDialog();
            TextBoxClientId.Text = form.UserId.ToString();
        }

        private void ButtonAddObject_Click(object sender, EventArgs e)
        {
            var form = new Object(mode);
            form.ShowDialog();
            dataGridView1.DataSource = ObjectFacade.GetObjectsDataTable();
        }

        private void ButtonSearch_Click(object sender, EventArgs e)
        {
            int id = 0;
            String name ="";
            try
            {
                id = int.Parse(TextBoxClientId.Text);
                name = TextBoxName.Text;
            }
            catch(Exception ex)
            {

            }
            String codeType = "";
            if(PROBtn.Checked == true)
            {
                codeType = "PRO";
            }
            else if(CANBtn.Checked == true)
            {
                codeType = "CAN";
            }
            else if (FINBtn.Checked == true)
            {
                codeType = "FIN";
            }
            else if (OPNBtn.Checked == true)
            {
                codeType = "OPN";
            }
            dataGridView1.DataSource = ObjectFacade.GetObjectsDataTable(id, name, codeType);
        }


        private void backBtn_Click(object sender, EventArgs e)
        {
            Close();
            nr_obj = (int)dataGridView1.CurrentRow.Cells[0].Value;
        }

        private void ButtonEditObject_Click(object sender, EventArgs e)
        {
            if(dataGridView1.CurrentRow != null)
            {
                int objId = (int)dataGridView1.CurrentRow.Cells[0].Value;
                if(ObjectFacade.FindObject(objId,out DataLayer.Object obj))
                {
                    var form = new Object(this.mode, obj);
                    form.ShowDialog();
                    dataGridView1.DataSource = ObjectFacade.GetObjectsDataTable();
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void ViewBtn_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int objId = (int)dataGridView1.CurrentRow.Cells[0].Value;
                if (ObjectFacade.FindObject(objId, out DataLayer.Object obj))
                {
                    var form = new Object("VIEW", obj);
                    form.ShowDialog();
                    dataGridView1.DataSource = ObjectFacade.GetObjectsDataTable();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = new Requests(mode);
            form.ShowDialog();
        }

        private void AddReqBtn_Click(object sender, EventArgs e)
        {
            var form = new Request();
            form.ShowDialog();
        }
    }
}
