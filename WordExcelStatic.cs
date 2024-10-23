using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Media;
using Excel = Microsoft.Office.Interop.Excel;
using Word = Microsoft.Office.Interop.Word;

namespace Project_3___Arcade
{
    public static class WordExcelStatic
    {
        //public static void PrintHighScore(Gebruiker gebruiker, string game, string highScore)
        //{
        //    if (highScore == null)
        //    {
        //        highScore = "0";
        //    }
        //    Word.Application wordApp = null;
        //    Word.Document wordDoc = null;
        //    try
        //    {


        //        wordApp = new Word.Application();
        //        wordDoc = wordApp.Documents.Add(Environment.CurrentDirectory + @"\Highscore sjabloon.dotx");
        //        foreach (Word.Bookmark bm in wordDoc.Bookmarks)
        //        {
        //            switch (bm.Name)
        //            {
        //                case "naam": bm.Range.Text = gebruiker.Gebruikersnaam; break;
        //                case "score": bm.Range.Text = highScore; break;
        //                case "game": bm.Range.Text = game; break;

        //            }

        //        }

        //        wordDoc.SaveAs(Environment.CurrentDirectory + @"..\..\..\Word\highscore" + game + gebruiker.Gebruikersnaam + ".docx");
        //        wordDoc.Close(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Er is iets fout gelopen:" + ex.Message);
        //    }
        //    finally
        //    {

        //        wordApp.Quit();
        //        wordDoc = null;
        //        wordApp = null;
        //    }
        //}
        //public static void PrintExcel(List<Gebruiker> lijstPlayers)
        //{
        //    Excel.Application xlApp = null;
        //    Excel.Workbook xlWorkbook = null;
        //    Excel.Worksheet xlWorksheet = null;
        //    Excel.Chart chart;
        //    Excel.ChartObject chartobj;
        //    Excel.Range chartRange = null;
        //    try
        //    {
        //        xlApp = new Excel.Application();
        //        xlWorkbook = xlApp.Workbooks.Add();
        //        DataSet dataSet = new DataSet();
        //        dataSet.Tables.Add(ConvertToDataTable(lijstPlayers));
        //        foreach (System.Data.DataTable table in dataSet.Tables)
        //        {
        //            //Add a new worksheet to workbook with the Datatable name  
        //            xlWorksheet = xlWorkbook.Sheets.Add();
        //            xlWorksheet.Name = table.TableName;

        //            // add all the columns  
        //            for (int i = 1; i < table.Columns.Count + 1; i++)
        //            {
        //                xlWorksheet.Cells[1, i] = table.Columns[i - 1].ColumnName;
        //            }

        //            // add all the rows  
        //            for (int j = 0; j < table.Rows.Count; j++)
        //            {
        //                for (int k = 0; k < table.Columns.Count; k++)
        //                {
        //                    xlWorksheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
        //                }
        //            }
        //        }
        //        // Tetris 
        //        chartobj = xlWorksheet.ChartObjects().Add(400, 25, 400, 250);
        //        chart = chartobj.Chart;
        //        int maxrij = xlWorksheet.UsedRange.Rows.Count;
        //        chartRange = xlWorksheet.get_Range("A1:B" + maxrij.ToString());
        //        chart.SetSourceData(chartRange);

        //        chart.ChartType = Excel.XlChartType.xlColumnClustered;

        //        chart.HasTitle = true;
        //        chart.ChartTitle.Text = "Tetris";

        //        chart.Axes(Excel.XlAxisType.xlCategory).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Players";
        //        chart.Axes(Excel.XlAxisType.xlValue).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlValue).AxisTitle.Characters.Text = "Highscores";
        //        // endless runner 
        //        chartobj = xlWorksheet.ChartObjects().Add(825, 25, 400, 250);
        //        chart = chartobj.Chart;
        //        chartRange = xlWorksheet.get_Range("A1:A" + maxrij.ToString() + ";C1:C" + maxrij.ToString());
        //        chart.SetSourceData(chartRange);

        //        chart.ChartType = Excel.XlChartType.xlColumnClustered;

        //        chart.HasTitle = true;
        //        chart.ChartTitle.Text = "Endless Runner";

        //        chart.Axes(Excel.XlAxisType.xlCategory).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Players";
        //        chart.Axes(Excel.XlAxisType.xlValue).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlValue).AxisTitle.Characters.Text = "Highscores";
        //        // Shooter
        //        chartobj = xlWorksheet.ChartObjects().Add(400, 300, 400, 250);
        //        chart = chartobj.Chart;
        //        chartRange = xlWorksheet.get_Range("A1:A" + maxrij.ToString() + ";D1:D" + maxrij.ToString());
        //        chart.SetSourceData(chartRange);
        //        chart.ChartType = Excel.XlChartType.xlColumnClustered;

        //        chart.HasTitle = true;
        //        chart.ChartTitle.Text = "Zombie shooter";

        //        chart.Axes(Excel.XlAxisType.xlCategory).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Players";
        //        chart.Axes(Excel.XlAxisType.xlValue).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlValue).AxisTitle.Characters.Text = "Highscores";
        //        // Flappy
        //        chartobj = xlWorksheet.ChartObjects().Add(825, 300, 400, 250);
        //        chart = chartobj.Chart;
        //        chartRange = xlWorksheet.get_Range("A1:A" + maxrij.ToString() + ";E1:E" + maxrij.ToString());
        //        chart.SetSourceData(chartRange);

        //        chart.ChartType = Excel.XlChartType.xlColumnClustered;

        //        chart.HasTitle = true;
        //        chart.ChartTitle.Text = "Flappy bird";

        //        chart.Axes(Excel.XlAxisType.xlCategory).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Players";
        //        chart.Axes(Excel.XlAxisType.xlValue).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlValue).AxisTitle.Characters.Text = "Highscores";
        //        // Snake
        //        chartobj = xlWorksheet.ChartObjects().Add(400, 575, 400, 250);
        //        chart = chartobj.Chart;
        //        chartRange = xlWorksheet.get_Range("A1:A" + maxrij.ToString() + ";F1:F" + maxrij.ToString());
        //        chart.SetSourceData(chartRange);

        //        chart.ChartType = Excel.XlChartType.xlColumnClustered;

        //        chart.HasTitle = true;
        //        chart.ChartTitle.Text = "Snake";

        //        chart.Axes(Excel.XlAxisType.xlCategory).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlCategory).AxisTitle.Characters.Text = "Players";
        //        chart.Axes(Excel.XlAxisType.xlValue).HasTitle = true;
        //        chart.Axes(Excel.XlAxisType.xlValue).AxisTitle.Characters.Text = "Highscores";

        //        xlWorkbook.SaveAs(Environment.CurrentDirectory + @"..\..\..\Excel\highscores.xlsx");
        //        xlWorkbook.Close();
        //        MessageBox.Show("Het overzicht is afgeprint!", "Succes!", MessageBoxButton.OK, MessageBoxImage.Information);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Er is iets fout gelopen:" + ex.Message);
        //    }
        //    finally
        //    {
        //        xlApp.Quit();
        //        chartRange = null;
        //        chart = null;
        //        chartobj = null;
        //        xlWorksheet = null;
        //        xlWorkbook = null;
        //        xlApp = null;
        //    }
        //}

        //public static System.Data.DataTable ConvertToDataTable(List<Gebruiker> data)
        //{
        //    // creating a data table instance and typed it as our incoming model   
        //    // as I make it generic, if you want, you can make it the model typed you want.  
        //    System.Data.DataTable dataTable = new System.Data.DataTable(typeof(Gebruiker).Name);

        //    // Loop through all the properties              
        //    // Adding Column name to our datatable  
        //    dataTable.Columns.Add("Naam");
        //    dataTable.Columns.Add("TetrisScore");
        //    dataTable.Columns.Add("RunnerScore");
        //    dataTable.Columns.Add("ShooterScore");
        //    dataTable.Columns.Add("FlappyScore");
        //    dataTable.Columns.Add("SnakeScore");
        //    // Adding Row and its value to our dataTable  
        //    foreach (Gebruiker user in data)
        //    {
        //        var scoreTetris = Datamanager.GetScoreTetrisGebruiker(user.UserID);
        //        var scoreRunner = Datamanager.GetScoreEndlessRunnerGebruiker(user.UserID);
        //        var scoreShooter = Datamanager.GetScoreZombieShooterGebruiker(user.UserID);
        //        var scoreFlappy = Datamanager.GetScoreFlappyBirdGebruiker(user.UserID);
        //        var scoreSnake = Datamanager.GetScoreSnakeGebruiker(user.UserID);
        //        var values = new object[6];
        //        values[0] = user.Gebruikersnaam;
        //        if (scoreTetris != null)
        //            values[1] = scoreTetris.Score;
        //        else values[1] = 0;

        //        if (scoreRunner != null)
        //            values[2] = scoreRunner.Score;
        //        else values[2] = 0;

        //        if (scoreShooter != null)
        //            values[3] = scoreShooter.Score;
        //        else values[3] = 0;

        //        if (scoreFlappy != null)
        //            values[4] = scoreFlappy.Score;
        //        else values[4] = 0;

        //        if (scoreSnake != null)
        //            values[5] = scoreSnake.Score;
        //        else values[5] = 0;

        //        // Finally add value to datatable    
        //        dataTable.Rows.Add(values);
        //    }
        //    return dataTable;
        //}
    }
}