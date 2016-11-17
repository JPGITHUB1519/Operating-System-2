﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Utils;
namespace FileSystem
{
    public partial class FrmMantPersonas : Form
    {
        List<String> lista_barrio = new List<String>();
        List<String> lista_urbanizacion = new List<String>();
        List<String> lista_seccion_dm = new List<String>();
        List<String> lista_barrio_dm = new List<String>();
        List<String> lista_urbanizacion_dm = new List<String>();
        List<String> lista_distrito_municipal = new List<String>();

        public FrmMantPersonas()
        {
            InitializeComponent();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            char sep = Utils.Utilities.sep;
            bool error = false;
            string data = "";
            Persona pe = new Persona();
            pe.Idpersona = Convert.ToInt32(txtcodigo.Text.Trim());
            pe.Nombre = txtnombre.Text.Trim();
            pe.Apellido = txtapellido.Text.Trim();
            pe.Sexo = Convert.ToChar(cmbsexo.Text);
            pe.Fecha_nacimiento = dtfecha.Value.ToShortDateString();
            pe.Estado_civil = cmbestadocivil.Text.Trim();
            pe.Ocupacion = txtocupacion.Text.Trim();
            pe.Idbarrio = Convert.ToInt32(cmbbarrio.Text);
            pe.Idurbanizacion = Convert.ToInt32(cmburbanizacion.Text);
            pe.Idseccion_dm = Convert.ToInt32(cmbsecciondm.Text);
            pe.Idbarrio_dm = Convert.ToInt32(cmbbarriodm.Text);
            pe.Idurbanizacion_dm = Convert.ToInt32(cmburbanizaciondm.Text);
            pe.Iddistrito_municipal = Convert.ToInt32(cmbdistritomunicipal.Text);
            pe.Is_vivo = chkvivo.Checked;
            pe.insertarPersona(pe);
            MessageBox.Show("Exito", "FileSystem", MessageBoxButtons.OK, MessageBoxIcon.Information);


        }

        private void FrmMantPersonas_Load(object sender, EventArgs e)
        {
            // filling combo box barrio
            lista_barrio = Utilities.readIdsFromFile(Utilities.barrio_dir);
            foreach (string id in lista_barrio)
            {
                cmbbarrio.Items.Add(id);
            }

            // filling combo box urbanizacion
            lista_urbanizacion = Utilities.readIdsFromFile(Utilities.urbanizacion_dir);
            foreach (string id in lista_urbanizacion)
            {
                cmburbanizacion.Items.Add(id);
            }

            // filling combo box seccion_dm
            lista_seccion_dm = Utilities.readIdsFromFile(Utilities.seccion_dm_dir);
            foreach (string id in lista_seccion_dm)
            {
                cmbsecciondm.Items.Add(id);
            }

            // filling combo box barrio_dm
            lista_barrio_dm = Utilities.readIdsFromFile(Utilities.barrio_dm_dir);
            foreach (string id in lista_barrio_dm)
            {
                cmbbarriodm.Items.Add(id);
            }

            // filling combo box urbanizacion_dm
            lista_urbanizacion_dm = Utilities.readIdsFromFile(Utilities.urbanizacion_dm_dir);
            foreach (string id in lista_urbanizacion_dm)
            {
                cmburbanizaciondm.Items.Add(id);
            }

            // filling combo box distrito_municipal
            lista_distrito_municipal = Utilities.readIdsFromFile(Utilities.distrito_municipal_dir);
            foreach (string id in lista_distrito_municipal)
            {
                cmbdistritomunicipal.Items.Add(id);
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            Utilities.deleteFromFileById(Utilities.persona_dir, Convert.ToInt32(txtcodigo.Text));
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            FrmConsPersona con_per = new FrmConsPersona();
            con_per.Show();
        }
    }
}