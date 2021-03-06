﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utils
{
    public class DistritoMunicipal
    {
        int iddistrito;
        string nombre;
        string localizacion;
        float area;
        string punto_cardinal;
        int idprovincia;

        public int Iddistrito
        {
            get { return iddistrito; }
            set { iddistrito = value; }
        }

        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public string Localizacion
        {
            get { return localizacion; }
            set { localizacion = value; }
        }

        public float Area
        {
            get { return area; }
            set { area = value; }
        }

        public string Punto_cardinal
        {
            get { return punto_cardinal; }
            set { punto_cardinal = value; }
        }

        public int Idprovincia
        {
            get { return idprovincia; }
            set { idprovincia = value; }
        }

        public void insertarDistritoMunicipal(DistritoMunicipal dist, bool is_editar = false)
        {
            char sep = Utils.Utilities.sep;
            string dir = Utilities.distrito_municipal_dir;
            string data = dist.iddistrito.ToString() + sep + dist.Idprovincia + sep + dist.Nombre + sep + dist.Localizacion + sep + dist.Area.ToString() + sep + dist.Punto_cardinal;
            if(is_editar)
                Utilities.updateFromFileById(dir, dist.iddistrito, data);
            else
                Utilities.writeSingleLineToFile(dir, data);
        }
    }
}
