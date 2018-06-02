using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            if (string.IsNullOrEmpty(text))
            {
                label1.Text = "String is not valid";
            }
            else
            {
                text = text.Trim().ToLower();
                var splitText = text.Split(' ');
                var shape = splitText[2];

                string measurement = string.Empty;
                string amount1 = string.Empty;
                string amount2 = string.Empty;
                Dictionary<string, string> values = new Dictionary<string, string>();

                switch (shape)
                {
                    case "circle":
                        //measurement = splitText[5];
                        //amount1 = splitText[7];
                        if (splitText.Count() == 8)
                        {
                            values.Add(splitText[5], splitText[7]);
                        }
                        else
                        {
                            label1.Text = "Entered string is not in correct format" + "\n" + "E.g.: Draw a circle with a radius of 100";
                        }
                        break;
                    case "square":
                        //measurement = "side";
                        //amount1 = splitText[8];
                        if (splitText.Count() == 9)
                            values.Add("side", splitText[8]);
                        else
                            label1.Text = "Entered string is not in correct format" + "\n" + "E.g.: Draw a square with a side length of 200";
                        break;
                    case "rectangle":
                        if (splitText.Count() == 13)
                        {
                            int index = 0;
                            foreach (var item in splitText)
                            {
                                index += 1;
                                if (item == "width")
                                {
                                    values.Add("width", splitText[index + 1]);
                                    //amount1 = splitText[index + 1];
                                }
                                else if (item == "height")
                                {
                                    //amount2 = splitText[index + 1];
                                    values.Add("height", splitText[index + 1]);
                                }
                            }
                        }
                        else
                        {
                            label1.Text = "Entered string is not in correct format" + "\n" + "E.g.: Draw a rectangle with a width of 250 and a height of 400";
                        }
                        break;
                    default:
                        break;
                }

                drawShape(shape, values);
            }

        }

        private bool drawShape(string shape, Dictionary<string, string> dict)
        {
            if (dict.Count > 0)
            {
                var checkAmount = dict.Select(pair => pair.Value).FirstOrDefault();
                if (!IsNumeric(checkAmount))
                {
                    label1.Text = "Amount is invalid";
                    return false;
                }
                else
                {
                    if (shape == "circle")
                    {
                        #region paramerter                        
                        if (dict.ContainsKey("radius"))
                        {
                            if (IsNumeric(dict["radius"]))
                            {
                                int value = Convert.ToInt32(dict["radius"]);
                                drawCircle(value);
                            }
                            else
                            {
                                label1.Text = "Amount is invalid";
                                return false;
                            }
                        }

                        #endregion

                    }
                    else if (shape == "square")
                    {
                        #region parmeter


                        if (dict.ContainsKey("side"))
                        {
                            if (IsNumeric(dict["side"]))
                            {
                                int value = Convert.ToInt32(dict["side"]);

                                drawRectange(value, value);
                            }
                            else
                            {
                                label1.Text = "Amount is invalid";
                                return false;
                            }
                        }
                        #endregion
                    }
                    else if (shape == "rectangle")
                    {
                        int height;
                        int width;

                        #region parameter                        
                        if (dict.ContainsKey("width") && dict.ContainsKey("height"))
                        {
                            if (IsNumeric(dict["width"]))
                            {
                                width = Convert.ToInt32(dict["width"]);
                            }
                            else
                            {
                                label1.Text = "Amount is invalid";
                                return false;
                            }

                            if (IsNumeric(dict["height"]))
                            {
                                height = Convert.ToInt32(dict["height"]);
                            }
                            else
                            {
                                label1.Text = "Amount is invalid";
                                return false;
                            }

                        }
                        else
                        {
                            label1.Text = "Amount is invalid";
                            return false;
                        }
                        #endregion
                        label1.Text = "Width: " + width + "Height: " + height;

                        drawRectange(height, width);
                    }
                }
            }

            return true;
        }

        private void drawRectange(int height, int width)
        {
            Graphics myGraphics = base.CreateGraphics();
            myGraphics.Clear(Color.White);
            // Create pen.
            Pen blackPen = new Pen(Color.Red, 3);
            // Create rectangle.
            Rectangle rect = new Rectangle(100, 200, width, height);
            // Draw rectangle to screen.
            myGraphics.DrawRectangle(blackPen, rect);
            label1.Text = "";
        }

        private void drawCircle(int value)
        {
            Graphics myGraphics = base.CreateGraphics();
            myGraphics.Clear(Color.White);

            Pen myPen = new Pen(Color.Red);
            SolidBrush mySolidBrush = new SolidBrush(Color.Red);
            myGraphics.DrawEllipse(myPen, value, value, value, value);
            label1.Text = "";
        }

        public bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }
    }
}
