﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;

namespace Utils
{
    public class Utilities
    {
        public static char sep = ',';
        public static string provincia_dir = "file_provincia.txt";
        public static string municipio_dir = "file_municipio.txt";
        public static string barrio_dir = "file_barrio.txt";
        public static string urbanizacion_dir = "file_urbanizacion.txt";
        public static string distrito_municipal_dir = "file_distrito_municipal.txt";
        public static string seccion_dm_dir = "file_seccion_dm.txt";
        public static string barrio_dm_dir = "file_barrio_dm.txt";
        public static string urbanizacion_dm_dir = "file_urbanizacion_dm.txt";
        public static string persona_dir = "file_persona.txt";
        List<String> lista = new List<String>();
   

        public static List<string> readFileByList(string dir)
        {
            int counter = 0;
            string line;
            List<string> lista = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(dir);
            while ((line = file.ReadLine()) != null)
            {
                lista.Add(line);
                counter++;
            }
            file.Close();

            return lista;
        }

        // read a file and convert it into list but specifiying the columns
        public static List<string> readFileByListSpecified(string dir, List<int>columns)
        {
            int column_counter = 0;
            string line;
            string [] splited;
            string new_line = "";
            List<string> lista = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(dir);
            while ((line = file.ReadLine()) != null)
            {
                splited = line.Split(Utilities.sep);
                column_counter = 0;
                for (int x = 0; x < splited.Length; x++)
                {
                    if (columns.Contains(x))
                    {
                        if (column_counter != columns.Count - 1)
                        {
                            new_line = new_line + splited[x] + sep;
                            column_counter++;
                        }
                        else
                        {
                            new_line = new_line + splited[x];
                            column_counter++;
                        }
                    }
                }
                
                lista.Add(new_line);
                new_line = "";
            }
            file.Close();

            return lista;
        }

        public static string[] fileToStringArray(string dir)
        {
            string[] lines = File.ReadAllLines("file.txt");
            return lines;
        }

        public static string fileToString(string dir)
        {
            string content = File.ReadAllText(dir);
            return content;
        }        

        public static void writeListToFile(string dir, List<string> lista)
        {
            using (System.IO.StreamWriter file = File.AppendText(dir))
            {
                foreach (string line in lista)
                {
                    file.WriteLine(line);
                }
            }
        }

        public static void writeSingleLineToFile(string dir, string line)
        {
            using (System.IO.StreamWriter file = File.AppendText(dir))
            {
               file.WriteLine(line);
            }
        }

        public static void deleteFromFileById(string dir, int id)
        {
            string line = null;
            string line_to_delete = id.ToString();
            string id_file;
            string copy_file_name = "copy" + dir;
            string copy = "";
            using (StreamReader reader = new StreamReader(dir))
            {
                using (StreamWriter writer = new StreamWriter(copy_file_name))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        id_file = line.Split(Utilities.sep)[0];
                        if (String.Compare(id_file, id.ToString()) == 0)
                            continue;
                        writer.WriteLine(line);
                    }
                }
            }
            copy = fileToString(copy_file_name);
            File.Delete(copy_file_name);
            File.WriteAllText(dir, String.Empty);
            File.WriteAllText(dir, copy);
        }

        public static void updateFromFileById(string dir, int id, string modification)
        {
            string line = null;
            string line_to_delete = id.ToString();
            string id_file;
            string copy_file_name = "copy" + dir;
            string copy = "";
            using (StreamReader reader = new StreamReader(dir))
            {
                using (StreamWriter writer = new StreamWriter(copy_file_name))
                {
                    while ((line = reader.ReadLine()) != null)
                    {
                        id_file = line.Split(Utilities.sep)[0];
                        if (String.Compare(id_file, id.ToString()) == 0)
                        {
                            writer.WriteLine(modification);
                        }  
                        else
                        {
                            writer.WriteLine(line);
                        }
                        
                    }
                }
            }
            copy = fileToString(copy_file_name);
            File.Delete(copy_file_name);
            File.WriteAllText(dir, String.Empty);
            File.WriteAllText(dir, copy);
        }

        public static List<String> readIdsFromFile(string dir)
        {
            string line;
            string id;
            List<string> lista = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(dir);
            while ((line = file.ReadLine()) != null)
            {
                id = line.Split(Utilities.sep)[0];
                lista.Add(id);
            }
            file.Close();
            return lista;
        }

        // fill datatable from a file
        public static DataTable fileToDataTable(string dir)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(dir);
            string[] columnnames = file.ReadLine().Split(Utilities.sep);
            DataTable dt = new DataTable();
            foreach (string c in columnnames)
            {
                dt.Columns.Add(c);
            }
            string newline;
            while ((newline = file.ReadLine()) != null)
            {
                DataRow dr = dt.NewRow();
                string[] values = newline.Split(Utilities.sep);
                for (int i = 0; i < values.Length; i++)
                {
                    dr[i] = values[i];
                }
                dt.Rows.Add(dr);
            }
            file.Close();
            return dt;
        }

        // Convert to DataTable.
        public static DataTable ConvertListToDataTable(List<string> lista)
        {
            // New table.
            DataTable dt = new DataTable();
            string[] string_splited;
            int cont = 0;
            foreach (string element in lista)
            {
                // first row are the columns
                string_splited = element.Split(Utilities.sep);
                if (cont == 0)
                {
                    foreach (string column in string_splited)
                    {
                        dt.Columns.Add(column, typeof(string));
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < string_splited.Length; i++)
                    {
                        dr[i] = string_splited[i];
                    }
                    dt.Rows.Add(dr);
                }
                cont++;

            }

            return dt;
        }

        public static List<string> getPersonByNameFromFile(string name)
        {
            string dir = Utilities.persona_dir;
            string line;
            string[] line_split;
            List<string> lista = new List<string>();
            System.IO.StreamReader file = new System.IO.StreamReader(dir);
            while ((line = file.ReadLine()) != null)
            {
                // name column 1
                line_split = line.Split(Utilities.sep);
                if(line_split[1] == name)
                    lista.Add(line);
            }
            file.Close();
            return lista;
        }

        public static List<string> filterListByPersonName(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string []array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[1] == filter)
                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByPersonApellido(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[2] == filter)
                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByPersonSexo(List<string> lista, char filter)
        {
            filter = char.ToLower(filter);
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[3] == filter.ToString())
                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByPersonEstadoCivil(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[5] == filter.ToString())
                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByPersonAge(List<string> lista, int edad, Dictionary<string,bool> dic)
        {
            string opcion="";
            if (dic["ismayor"] == true)
                opcion = "mayor";
            if (dic["ismenor"] == true)
                opcion = "menor";
            if (dic["equal"] == true)
                opcion = "equal";
            List<string> list_filted = new List<string>();
            string[] array_splited;
            DateTime now = DateTime.Today;
            DateTime string_to_date;
            int calculated_age;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                string_to_date = DateTime.Parse(array_splited[4]);
                calculated_age = now.Year - string_to_date.Year;
                //Console.WriteLine(calculated_age);
                if (opcion == "mayor")
                {
                    if (calculated_age > edad)
                    {
                        list_filted.Add(line);
                    }
                }
                if (opcion == "menor")
                {
                    if (calculated_age < edad)
                    {
                        list_filted.Add(line);
                    }
                }
                if (opcion == "equal")
                {
                    if (calculated_age == edad)
                    {
                        list_filted.Add(line);
                    }
                }      
            }
            return list_filted;
        }

        public static List<string> filterListByPersonOcupacion(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[6] == filter)

                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByBarrio(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[7] == filter)

                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByUrbanizacion(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[8] == filter)

                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListBySeccionDM(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[9] == filter)

                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByBarrioDM(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[10] == filter)

                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByUrbanizacionDM(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[11] == filter)

                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static string ejecutar(string[] arr)
        {
            List<String> lista = new List<String>();
            lista = Utilities.readFileByList(Utilities.persona_dir);
            if (arr[10] == "0") { }
            else
            {
                lista = Utilities.filterListByPersonName(lista, arr[10]);
            }
            if (arr[9] == "0") { }
            else
            {
                lista = Utilities.filterListByPersonApellido(lista, arr[9]);
            }
            if (arr[8] == "0") { }
            else
            {
                char sexo = char.Parse(arr[8]);
                lista = Utilities.filterListByPersonSexo(lista, sexo);
            }
            if (arr[7] == "0") { }
            else
            {
                lista = lista = Utilities.filterListByPersonEstadoCivil(lista, arr[7]);
            }
            if (arr[6] == "0") { }
            else
            {
                lista = Utilities.filterListByPersonOcupacion(lista, arr[6]);
            }
            /*   if (!string.IsNullOrEmpty(txtedad.Text.Trim()))
               {
                   Dictionary<string, bool> dic = new Dictionary<string, bool>();
                   dic.Add("ismayor", false);
                   dic.Add("ismenor", false);
                   dic.Add("equal", false);
                   if (rbmayor.Checked)
                       dic["ismayor"] = true;
                   if (rbmenor.Checked)
                       dic["ismenor"] = true;
                   if (rbigual.Checked)
                       dic["igual"] = true;
                   this.lista = Utilities.filterListByPersonAge(this.lista, Convert.ToInt32(txtedad.Text.Trim()), dic);
               }*/
            if (arr[5] == "0") { } else
              {
                  lista = Utilities.filterListByBarrio(lista,arr[5]);
              }
        if (arr[4] == "0") { } else
              {
                  lista = Utilities.filterListByUrbanizacion(lista, arr[4]);
              }
              if (arr[3] == "0") { } else
              {
                  lista = Utilities.filterListBySeccionDM(lista, arr[3]);
              }
              if (arr[2] == "0") { } else
              {
                  lista = Utilities.filterListByBarrioDM(lista, arr[2]);
              }
             if (arr[1] == "0") { } else
              {
                  lista = Utilities.filterListByUrbanizacionDM(lista, arr[1]);
              }
             if (arr[0] == "0") { } else
             {    lista = Utilities.filterListByDistritoDM(lista,arr[0]);
              }
   
            // adding columns
          //  lista.Insert(0, "idpersona,nombre,apellido,sexo,fecha_nacimiento,estado_civil,ocupacion,idbarrio,idurbanizacion,idseccion_dm,idbarrio_dm,idurbanizacion_dm,iddistrito_municipal,isvivo");
            string dog = string.Join(",", lista.ToArray());
            return dog;
        }

        public static List<string> filterListByDistritoDM(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[12] == filter)

                    list_filted.Add(line);
            }
            return list_filted;
        }

        public static List<string> filterListByVivo(List<string> lista, string filter)
        {
            List<string> list_filted = new List<string>();
            string[] array_splited;
            foreach (string line in lista)
            {
                array_splited = line.Split(Utilities.sep);
                if (array_splited[13] == filter)

                    list_filted.Add(line);
            }
            return list_filted;
        }


        public static bool checkExitsRecordById(int id, string dir)
        {
            List<string> lista_id = new List<string>();
            lista_id = readIdsFromFile(dir);
            return lista_id.Contains(id.ToString());
        }

        public static void print_lista(List<string> lista)
        {
            foreach (object element in lista)
            {
                Console.WriteLine(element);
            }
        }

        public static void printCollection<T>(IEnumerable<T> a)
        {    
           foreach(var i in a)
           {
                Console.WriteLine(i);
           }
        }
        public static void printList(List<int> lista)
        {
            foreach (int element in lista)
            {
                Console.WriteLine(element);
            }
        }

        public static Dictionary<string, object> selectQueryReader(string query)
        {
            string command;
            string universal_filter = "";
            string table = "";
            List<string> parameters = new List<string>();
            bool end_parameters = false;
            int pos_finished_parameters = 0;
            string[] splited;
            query = query.ToLower();
            splited = query.Split(' ');
            //Utilities.printCollection(splited);
   
            command = splited[0];
            Dictionary<string, object> filters = new Dictionary<string, object>();

            for (int i = 0; i < splited.Length; i++)
            {
                if (i == 0)
                {
                    command = splited[0];
                }
                if (i == 1)
                {
                    if (splited[i] == "distinct" || splited[i] == "all")
                    {
                        universal_filter = splited[i];
                        end_parameters = true;
                        continue;
                    }
                }

                if (i == 1 || end_parameters == true)
                {
                    if (splited[i] != "from")
                    {
                        parameters.Add(splited[i].Replace(",", ""));
                    }
                    else
                    {
                        end_parameters = false;
                        pos_finished_parameters = i;
                    }
                }

                if (i == pos_finished_parameters + 1)
                {
                    table = splited[pos_finished_parameters + 1];
                }

            }

            filters.Add("command", command);
            filters.Add("universal_filter", universal_filter);
            filters.Add("parameters", parameters);
            filters.Add("table", table);

            return filters;   
        }        
    }
}
