using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;


namespace GraphDemo
{
    class Plot
    {
        public Plot(Read rr, ComboBox xBox, ComboBox yBox, Chart chart)
        {   // comes here when Plot button of demo is clicked
            int indX = xBox.SelectedIndex;
            int indX1 = xBox.SelectedIndex + 1;
            int indY = yBox.SelectedIndex;
            float[,] data = rr.get_Data();
            int nLines = rr.get_nLines();
            string SeriesCode = "Series";

            chart.Series.Clear(); //ensure that the chart is empty
            chart.Legends.Clear(); // moved from below to try to get everything in loop, may have to move back

           chart.ChartAreas[0].AxisX.Minimum = 0;  // try this here, do we need for all x's? MyChart goes to chart--works

 /*      for (int i = 0; i < nElements; i++)  // have to get nElements public
           {
               
           }
 */
            chart.Series.Add("Series0");
            chart.Series[0].ChartType = SeriesChartType.Point;

            chart.Series.Add("Series1");
            chart.Series[1].ChartType=SeriesChartType.Point;  // was charttype.line

            chart.Series.Add("Series2");  // copy pasted this from above
            chart.Series[2].ChartType = SeriesChartType.Point;  // was charttype.line

    //       chart.Legends.Clear();
            for (int j = 0; j < nLines+1; j++)
            {
                chart.Series[0].Points.AddXY(data[j, indX], data[j, indY]);
                chart.Series[1].Points.AddXY(data[j, indX], data[j, indY]);
                chart.Series[2].Points.AddXY(data[j, indX1], data[j, indY]);  // copy pasted from line above
            }
        }
    }
}
