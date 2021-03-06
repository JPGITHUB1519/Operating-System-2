﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace FileSystem
{
    public partial class frmreportes : Form
    {
        string nombre_reporte;
        DataTable dt;
        string report_dataset_name;
        public frmreportes()
        {
            InitializeComponent();
        }
        public frmreportes(string nombre_reporte, DataTable dt, string report_dataset_name)
        {
            InitializeComponent(); ;
            this.nombre_reporte = nombre_reporte;
            this.dt = dt;
            this.report_dataset_name = report_dataset_name;

        }
        
        private void frmreportes_Load(object sender, EventArgs e)
        {
            this.reportViewer1.LocalReport.DataSources.Clear();
            dataset ds = new dataset();
            DataTable dt = new DataTable();
            dt = Utils.Utilities.fileToDataTable(Utils.Utilities.provincia_dir);
            ReportDataSource rds = new ReportDataSource(this.report_dataset_name, this.dt);
            //MessageBox.Show(dt.Rows.Count.ToString());
            //this.reportViewer1.LocalReport.ReportEmbeddedResource = "FileSystem.reporte_provincia.rdlc";
            this.reportViewer1.LocalReport.ReportEmbeddedResource = this.nombre_reporte;
            this.reportViewer1.LocalReport.DataSources.Add(rds);
            this.reportViewer1.RefreshReport();
        }
    }
}
