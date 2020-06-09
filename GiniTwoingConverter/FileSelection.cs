using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using weka.core;
using weka.core.converters;

namespace GiniTwoingConverter
{
    public partial class FileSelection : Form
    {
        #region [VARIABLES]
        private List<string[]> categories;
        private string twoingPath = Directory.GetCurrentDirectory() + @"/Twoing.arff";
        private string giniPath = Directory.GetCurrentDirectory() + @"/Gini.arff";
        #endregion

        public FileSelection()
        {
            InitializeComponent();
        }


        #region [PRIVATE EVENTS]

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            categories = new List<string[]>();
            txtInfo.Text = "";
            btnTwoing.Enabled = false;
            btnGini.Enabled = false;
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                txtPath.Text = openFileDialog.FileName;
                btnTwoing.Enabled = true;
                btnGini.Enabled = true;
            }
            weka.core.Instances insts = new weka.core.Instances(new java.io.FileReader(txtPath.Text));
            Discreatization(insts);
        }

        private void BtnTwoing_Click(object sender, EventArgs e)
        {
            Process.Start(twoingPath);
        }

        private void BtnGini_Click(object sender, EventArgs e)
        {
            Process.Start(giniPath);
        }
        #endregion


        #region [PRIVATE METHODS]

        /// <summary>
        ///  Discreatize instance if the instance is numeric
        /// </summary>
        /// <param name="insts"></param>
        private void Discreatization(weka.core.Instances insts)
        {
            weka.filters.unsupervised.attribute.Discretize myDiscretize = new
                weka.filters.unsupervised.attribute.Discretize();
            myDiscretize.setInputFormat(insts);
            myDiscretize.setFindNumBins(true);
            insts = weka.filters.Filter.useFilter(insts, myDiscretize);
            DataPreparation(insts);
        }

        /// <summary>
        /// Determines all categories --> categories
        /// Determines category numbers of each attributes -->> categoryTypeNumber
        /// Determines target numbers and amounts of each categories of each attributes  -->> categoryTypeTargetNumber
        /// [i][j][k] i means attributes, j means  categories, k means targets
        /// </summary>
        /// <param name="insts"></param>
        private void DataPreparation(weka.core.Instances insts)
        {
            for (int i = 0; i < insts.numAttributes(); i++)
            {
                string[] categoryType = new string[insts.attribute(i).numValues()];
                for (int j = 0; j < insts.attribute(i).numValues(); j++)
                {
                    categoryType[j] = insts.attribute(i).value(j).ToString();
                }
                categories.Add(categoryType);
            }

            List<List<string>> lst = new List<List<string>>();

            for (int i = 0; i < insts.numInstances(); i++)
            {
                lst.Add(new List<string>());

                for (int j = 0; j < insts.instance(i).numValues(); j++)
                {
                    lst[lst.Count - 1].Add(insts.instance(i).toString(j));
                }
            }

            List<int[]> categoryTypeNumber = new List<int[]>();
            List<int[,]> categoryTypeTargetNumber = new List<int[,]>();
            for (int i = 0; i < categories.Count; i++)
            {
                categoryTypeNumber.Add(new int[categories[i].Length]);
                categoryTypeTargetNumber.Add(new int[categories[i].Length, categories[categories.Count - 1].Length]);
            }

            for (int i = 0; i < lst.Count; i++) //Satır
            {
                for (int j = 0; j < lst[i].Count; j++)//Sütün
                {
                    for (int k = 0; k < categories[j].Length; k++) //Kategori Sayısı
                    {
                        string targetValue = lst[i][lst[i].Count - 1];
                        if (lst[i][j].Contains(categories[j][k]))
                        {
                            categoryTypeNumber[j][k] += 1;
                            for (int trgt = 0; trgt < categories[categories.Count - 1].Length; trgt++)
                            {
                                if (targetValue == categories[categories.Count - 1][trgt])
                                {
                                    categoryTypeTargetNumber[j][k, trgt] += 1;
                                }
                            }
                        }
                    }
                }
            }
            Twoing(insts, categoryTypeNumber, categoryTypeTargetNumber);
            Gini(insts, categoryTypeNumber, categoryTypeTargetNumber);
            LogInfo("Dataset is saved.\r\n\r\n");
            LogInfo("TWOING : " + twoingPath + "\r\n\r\n");
            LogInfo("GINI : " + giniPath + "\r\n");
        }



        /// <summary>
        /// Calculates Pleft, Prigth, rigthTargets , PclassDivideTleft, PclassDivideTleft and teta numbers for each inctance
        /// Then calling the creating dataset function
        /// </summary>
        /// <param name="insts"></param>
        /// <param name="categoryTypeNumber"></param>
        /// <param name="categoryTypeTargetNumber"></param>
        private void Twoing(weka.core.Instances insts, List<int[]> categoryTypeNumber, List<int[,]> categoryTypeTargetNumber)
        {
            List<double[]> categoryTetaNumber = new List<double[]>();
            for (int i = 0; i < categoryTypeNumber.Count; i++)
            {
                categoryTetaNumber.Add(new double[categoryTypeNumber[i].Length]);
            }

            for (int i = 0; i < categoryTypeNumber.Count - 1; i++)
            {
                for (int j = 0; j < categoryTypeNumber[i].Length; j++)
                {
                    Double pLeft = Convert.ToDouble(categoryTypeNumber[i][j]) / Convert.ToDouble(insts.numInstances());
                    Double pRight = 1 - pLeft;
                    Double sumFunction = 0;
                    for (int k = 0; k < categoryTypeNumber[categoryTypeNumber.Count - 1].Length; k++)
                    {
                        Double PclassDivideTleft = Convert.ToDouble(categoryTypeTargetNumber[i][j, k]) / Convert.ToDouble(categoryTypeNumber[i][j]);
                        int sagtarafıntargetları = 0;
                        for (int h = 0; h < categoryTypeNumber[i].Length; h++)
                        {
                            if (h != j)
                            {
                                sagtarafıntargetları += categoryTypeTargetNumber[i][h, k];
                            }
                        }
                        Double PclassDivideTRigt = Convert.ToDouble(sagtarafıntargetları) / Convert.ToDouble((insts.numInstances() - categoryTypeNumber[i][j]));
                        sumFunction += Math.Abs(PclassDivideTleft - PclassDivideTRigt);
                    }

                    categoryTetaNumber[i][j] = 2 * pLeft * pRight * sumFunction;
                }
            }
            Instances fileInst = new Instances(insts);
            CreateNewDataset(fileInst, categoryTetaNumber, twoingPath);
        }

        /// <summary>
        /// Calculates giniLeft, giniRigth(these are fraction operations, so also calculates denominator and numerator of fractions)
        /// then calvulates gini result for each inctance
        /// Then calling the creating dataset function
        /// </summary>
        /// <param name="insts"></param>
        /// <param name="categoryTypeNumber"></param>
        /// <param name="categoryTypeTargetNumber"></param>
        private void Gini(weka.core.Instances insts, List<int[]> categoryTypeNumber, List<int[,]> categoryTypeTargetNumber)
        {
            List<double[]> giniResult = new List<double[]>();
            for (int i = 0; i < categoryTypeNumber.Count; i++)
            {
                giniResult.Add(new double[categoryTypeNumber[i].Length]);
            }

            for (int i = 0; i < categoryTypeNumber.Count - 1; i++)
            {
                int changeAttribute = 0;

                for (changeAttribute = 0; changeAttribute < categoryTypeNumber[i].Length; changeAttribute++)
                {
                    List<Double> giniLeftNumerator = new List<Double>();
                    List<Double> giniRigthNumerator = new List<Double>(); //numerator -> pay
                    Double giniRigthDenominator = 0;  //denominator -> payda
                    Double giniLeftDenominator = 0;
                    Double sumFunctionRight = 0;
                    Double giniRight = 0;
                    Double giniLeft = 0;
                    int categoryChange = 0;
                    Double sumFuntionLeft = 0;
                    for (categoryChange = 0; categoryChange < categoryTypeNumber[categoryTypeNumber.Count - 1].Length; categoryChange++)
                    {
                        giniLeftNumerator.Add(Convert.ToDouble(categoryTypeTargetNumber[i][changeAttribute, categoryChange]));
                    }

                    for (int index = 0; index < giniLeftNumerator.Count; index++)
                    {
                        giniLeftDenominator += giniLeftNumerator[index];
                    }

                    for (int z = giniLeftNumerator.Count; z > 0; z--)
                    {
                        sumFuntionLeft += Convert.ToDouble(Math.Pow(Math.Abs(giniLeftNumerator[z - 1] / giniLeftDenominator), 2));
                    }
                    giniLeft = 1 - sumFuntionLeft;

                    int[] targets = new int[giniLeftNumerator.Count];
                    for (int h = 0; h < categoryTypeNumber[i].Length; h++)
                    {
                        if (h != changeAttribute)
                        {
                            for (int z = 0; z < giniLeftNumerator.Count; z++)
                            {
                                giniRigthNumerator.Add(Convert.ToDouble(categoryTypeTargetNumber[i][h, z]));
                                targets[z] += categoryTypeTargetNumber[i][h, z];
                            }
                        }

                    }
                    for (int index = 0; index < giniRigthNumerator.Count; index++)
                    {
                        giniRigthDenominator += giniRigthNumerator[index];
                    }

                    for (int z = targets.Length; z > 0; z--)
                    {
                        sumFunctionRight += Convert.ToDouble(Math.Pow(Math.Abs(targets[z - 1] / giniRigthDenominator), 2));
                    }
                    giniRight = 1 - sumFunctionRight;

                    giniResult[i][changeAttribute] = Convert.ToDouble(1 / (giniLeftDenominator + giniRigthDenominator)) * Convert.ToDouble((giniLeftDenominator * giniLeft) + (giniRigthDenominator * giniRight));

                }
                Instances fileInst = new Instances(insts);
                CreateNewDataset(fileInst, giniResult, giniPath);
            }
        }


        /// <summary>
        /// Adds teta results of gini results to the list
        /// Change the attributes of the arff file
        /// Adds the attributes to arff file
        /// </summary>
        /// <param name="insts"></param>
        /// <param name="result"></param>
        /// <param name="path"></param>
        private void CreateNewDataset(weka.core.Instances insts, List<double[]> result, string path)
        {
            //Tetaları Listeye Ekle
            List<List<string>> lst = new List<List<string>>();
            for (int i = 0; i < insts.numInstances(); i++)
            {
                lst.Add(new List<string>());
                for (int j = 0; j < insts.instance(i).numValues() - 1; j++)
                {
                    string value = insts.instance(i).toString(j);
                    for (int k = 0; k < categories[j].Length; k++)
                    {
                        if (insts.instance(i).toString(j) == categories[j][k])
                        {
                            lst[lst.Count - 1].Add(String.Format("{0:0.00}", result[j][k]));
                            break;
                        }
                    }
                }
            }
            //Attiribute Değiştir
            for (int i = 0; i < insts.numAttributes() - 1; i++)
            {
                string name = insts.attribute(i).name().ToString();
                insts.deleteAttributeAt(i);
                weka.core.Attribute att = new weka.core.Attribute(name);
                insts.insertAttributeAt(att, i);
            }

            //Attiributeları yaz
            for (int i = 0; i < insts.numInstances(); i++)
            {
                for (int j = 0; j < insts.instance(i).numValues() - 1; j++)
                {
                    insts.instance(i).setValue(j, Convert.ToDouble(lst[i][j]));
                }
            }

            if (File.Exists(path)) File.Delete(path);
            var saver = new ArffSaver();
            saver.setInstances(insts);
            saver.setFile(new java.io.File(path));
            saver.writeBatch();
        }


        /// <summary>
        /// Showing the results on the UI
        /// </summary>
        /// <param name="ınfo"></param>
        private void LogInfo(string info)
        {
            txtInfo.Text += info;
        }

        #endregion


    }
}
