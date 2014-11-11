using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;


namespace GraphDemo
{
   class Plot  // add public--read is less accessible than plot, public partial same thing
    {
       // public Plot(Read rr, ComboBox xBox, ComboBox yBox, Chart chart)
        public Plot(Read rr,  Chart chart)
        {   // comes here when Plot button of demo is clicked
           // int indX = 1;// xBox.SelectedIndex;  // this will be 1 always
            //int indX1 = xBox.SelectedIndex + 1;
            //int indX2 = xBox.SelectedIndex + 2;
            //int indX3 = xBox.SelectedIndex + 3;
            int indY = 0; // yBox.SelectedIndex;          // this will be 0 always
            float[,] data = rr.get_Data();
            int nLines = rr.get_nLines();
            string[] ElemLabels = rr.get_Header();
            string SeriesCode ;
            string TmpS;

        // ElemLabels[0] = ""; // leaves the marker
            chart.Series.Clear(); //ensure that the chart is empty
           
            // chart.Legends.Clear(); // moved from below to try to get everything in loop, may have to move back
            // clear legends keeps them from showing, now starts with depth at 0, how to start at 1?

       //     chart.mark

            

           chart.ChartAreas[0].AxisX.Minimum = 0;  // try this here, do we need for all x's? MyChart goes to chart--works

 /*      for (int i = 0; i < nElements; i++)  // have to get nElements public
           {
               
           }
 */
           for (int i = 0; i < Global.nElements; i++)  // try start with i = 1 not 0, that causes out of range : chart.Series[i].ChartType = SeriesChartType.Point
           {
               TmpS = i.ToString() ;
               SeriesCode = "Series" + TmpS;
               chart.Series.Add(SeriesCode);
               chart.Series[i].ChartType = SeriesChartType.Point; // runs, but now there are lines from the X axis down to the points
               chart.Series[i].Name = ElemLabels[i];

           }


           //chart.Series.Add("Series0");   // this was X
           //chart.Series[0].ChartType = SeriesChartType.Point;

           //chart.Series.Add("Series1");  // this was the first Y
           //chart.Series[1].ChartType = SeriesChartType.Point;  // was charttype.line

           //chart.Series.Add("Series2");  // copy pasted this from above --- the second X? 
           //chart.Series[2].ChartType = SeriesChartType.Point;  // was charttype.line
 

    //       chart.Legends.Clear();
           
            //for (int j = 0; j < nLines+1; j++)
            //{
            //    chart.Series[0].Points.AddXY(data[j, indX], data[j, indY]); 
            // //   chart.Series[1].Points.AddXY(data[j, indX], data[j, indY]);   // same as line above? looks ok w/o
            //    chart.Series[2].Points.AddXY(data[j, indX1], data[j, indY]);  // copy pasted from line above
            //    chart.Series[3].Points.AddXY(data[j, indX2], data[j, indY]);  // copy pasted from line above
            //    chart.Series[4].Points.AddXY(data[j, indX3], data[j, indY]);  // copy pasted from line above
            //}

           for (int j = 0; j < nLines+1 ; j++)  //lines of results from 0 to ...
           {
               for (int indeX = 1; indeX < Global.nElements; indeX++)  // elem from 1 to ...    try index = 1, not 0
               {
                   chart.Series[indeX ].Points.AddXY(data[j, indeX], data[j, indY]); 
               }
           }


        }
    }
}
// gratuitous change
