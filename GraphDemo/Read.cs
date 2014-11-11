using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GraphDemo
{

    class Read
    {
        Plot pp;  // maybe these will give us the scope to get a plot going after read

        GraphDemo gd;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;   // taken frm graphdemo.designer.cs, made private
        private string[] header;
        private string[] ElemNames;
        private float[,] data;
        private int nLines;
        private int nColumns;
       // public  int nElements;        // will this include depth in the count?
        int[] indexes= new int[100];  // places in line where there are element names or concentrations
        string[] ElemLabels = new string[100];   // will start with 'depth at beginning'
        int q; // was i but took out 'for'

        public Read(Stream myStream)
        {
            string aux;
            string testval;  // for example a conc as a string
            string[] tmp;
            string tmp2;
            int tmptmp;
            int tmpint;
            string[] pieces;
            int jj;  // for indexes
            int v; // for index into elemnames
         //   int nElements;        // will this include depth in the count? --- move to global
           

            //read the file line by line
            // discard the first 6 lines
            StreamReader sr = new StreamReader(myStream);
            aux = sr.ReadLine();      // line 1
            aux = sr.ReadLine();      // line 2
            aux = sr.ReadLine();      // line 3
            aux = sr.ReadLine();      // line 4
            aux = sr.ReadLine();      // line 5
            aux = sr.ReadLine();      // line 6

          aux = sr.ReadLine();      // line 7  --- this contains the element names
            ElemNames = aux.Split(',');
            aux = ElemNames.GetValue(27).ToString(); // 16, 27, gets elem name
            // process to put names in array, or wait until we have analyzed elements found
            // by non null concentrations and go back and find the elements
            aux = sr.ReadLine();      // line 8  --- description -- intensity, conc, errors
            aux = sr.ReadLine();      // line 9  --- units channel, µg/g, etc.
            // now the actual sample data
            header = aux.Split(',');
            nColumns = header.Length;
            nLines = 0;
            jj = 0;
            Global.nElements = 0;
          //  while ((aux = sr.ReadLine()) != null)   // really only want to do one line to see what's measured
            aux = sr.ReadLine();
            {
                if (aux.Length > 0) nLines++;  // we have a line 
                // stuff in depth index
                indexes[jj] = 3;
                jj++;
                // look for non-null concentrations at certain columns
                for (int i = 16; i < 930;i+=11 )  // once into the real data, every 11 columns is new elem report
                {
                    tmp = aux.Split(',');
                    tmptmp = tmp.Length;
                    testval = tmp.GetValue(i).ToString();
                    if (i ==929) i = 929;
                    if (testval.Length > 0)
                    {
                        indexes[jj] = i;
                        jj++;
                        Global.nElements++;

                    }
                        
                }
                // stuff depth into the element label array so the original part of this program might be useful
                // this should be a 'numeric value' but the array is string
                ElemLabels[0] = ElemNames[3];
                // now find the elem labels
                v = 1;
                tmpint = indexes[v];
          //  while (indexes[v] != 0)   // != cannot compare object and int
                while (tmpint != 0)
                {
                    tmp2 = ElemNames[indexes[v]];
                    int l = tmp2.IndexOf("(");
                    if (l>0)
                    {
                        tmp2 = tmp2.Substring(0, l);
                    }
                    ElemLabels[v] = tmp2;
                    if (v == 10)
                    {
                        v = v;
                    }
                    v++;
                    tmpint = indexes[v];
                }
                   

            }
            // looks like it starts over on the file
            //read the numerical data from file in an array   (in TO an array?)
            data = new float[100, 20];  // start with 100 points max, 20 elements
            sr.BaseStream.Seek(0, 0);
            //sr.ReadLine();                  // does this get the first line?, let's do 9 altogether
            for (int i=0;i<9;i++)
            {
                aux=sr.ReadLine();
            }
            // now we are in the real data
            // read the distance at item 3 (frm 0, 'depth at beg'), then the concentrations, at places found before and stored in 'indexes'
            // we don't know number of lines so we'll read till eof or whatever, and store the number of lines
           // for (int i = 0; i < nLines; i++)
            nLines = -1;
            
            while ((aux=sr.ReadLine()) != null)   // there could be floats (87.6) or strings (<DL)
            {
                nLines++;
                pieces = aux.Split(',');
                // get the nElements worth of values or strings

                for (int i = 0; i < Global.nElements; i++)  // depth at index line,0
                {   
                   
                    try
                    {
                        data[nLines,i] = float.Parse(pieces[indexes[i]]);
                    }
                    catch (Exception err)  // warning err never used
                    {

                        data[nLines,i]=0;  // if <DL
                    }
                    if(nLines==4)
                    {
                        if (i == Global.nElements - 1)
                       {
                           i = i;
                       }
                    }
                  
                    
                }
            }
            sr.Close();
            //jj = jj;
            // set the series-- in plot.cs


           // // push the plot button--in ...
        //    GraphDemo.Plot();

       //    GraphDemo.btnPlot.PerformClick();  // can't get it to work
           Read rr;  // this helps next lines, but chart is not defined
          //  if (rr != null)                    // this is from GraphDemo.cs
            {
             //   Plot pl = new Plot(rr, chart);
            }
         //   else
            {
              //  MessageBox.Show("Error, no data to plot! Please load csv file");
        //        return;
            }
        }  // end of class read

 
        //functions used for retrieving the data
        public int get_nLines()
        {
            return nLines;
        }

        public int get_nColumns()
        {
            return nColumns;
        }

        public float[,] get_Data()
        {
            return data;
        }

        public string[] get_Header()  // instead of the header this finds, we want depth & element labels
        {
            return ElemLabels;
        }
    }
}
