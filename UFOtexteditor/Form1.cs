using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UFOtexteditor
{
    public partial class Form1 : Form
    {

  
        public Form1()
        {
            InitializeComponent();
            BtnRedo.Enabled = false;
            BtnUndo.Enabled = false;
        }

        Stack<string> _editingHistory = new Stack<string>();
        Stack<string> _undoHistory = new Stack<string>();
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Modified)
            {
                BtnUndo.Enabled = true;
                var txt = sender as TextBox;
                _editingHistory.Push((txt.Text));
            }

            var test = textBox1.SelectionStart;
        }

        private void BtnUndo_Click(object sender, EventArgs e)
        {
            _undoHistory.Push(_editingHistory.Pop());

            BtnRedo.Enabled = true;

            if (_editingHistory.Count != 0)
            {
                textBox1.Text = _editingHistory.Peek();
            }
            else
            {
                textBox1.Text = "";
            }
            BtnUndo.Enabled = _editingHistory.Count > 0;

        }

        private void BtnRedo_Click(object sender, EventArgs e)
        {
            _editingHistory.Push(_undoHistory.Pop());
            BtnRedo.Enabled = _undoHistory.Count > 0;
            textBox1.Text = _editingHistory.Peek();

        }
        //Nave NOT TO USE 
        //private void Btn_search_Click(object sender, EventArgs e)
        //{
        //    string searchStr = txtSearchbox.Text;
        //    string searchIn = textBox1.Text;
        //    int count = 0;
        //    //M A L Y A L A M   
        //    //A L Y
        //   int i = 0;

        //   string strVal = string.Empty;

        //    for (int j = 0; j < searchIn.Length; j++) // M , A
        //    {
        //        if (searchIn[j] == searchStr[i]) // False, true
        //        {
        //            count++;

        //            strVal = strVal + searchStr[i];

        //            i++;

        //            if (strVal.Length == searchStr.Length)
        //            {
        //                MessageBox.Show("Match Found");
        //                break;
        //            }
        //        }
        //        else if (count != 0)
        //        {
        //            count = 0;
        //            i = i - 1;
        //        }

        //    }

        //}


        private void Btn_search_Click(object sender, EventArgs e)
        {
            //step 1- Create a look up table for string need to search

            string searchStr = txtSearchbox.Text;
            string searchIn = textBox1.Text;

            KMPSearch(searchStr, searchIn);
        }

        /// <summary>
        /// KMP algo to find a pattern in a string.
        /// </summary>
        /// <param name="pat"></param>
        /// <param name="txt"></param>

        void KMPSearch(string pat, string txt)
        {
            int M = pat.Length; // a a b b a a
            int N = txt.Length;//  aabaabbaaaa

            // create lps[] that will hold the longest 
            // prefix suffix values for pattern 
            int[] lps = new int[M];
            int j = 0; // index for pat[] 

            // Preprocess the pattern (calculate lps[] 
            // array) 
            computeLPSArray(pat, M, lps);

            int i = 0; // index for txt[] 
            while (i < N)
            {
                if (pat[j] == txt[i])
                {
                    j++;
                    i++;
                }
                if (j == M)
                {
                    Console.Write("Found pattern "
                                  + "at index " + (i - j));
                    j = lps[j - 1];
                }

                // mismatch after j matches 
                else if (i < N && pat[j] != txt[i])
                {
                    // Do not match lps[0..lps[j-1]] characters, 
                    // they will match anyway 
                    if (j != 0)
                        j = lps[j - 1];
                    else
                        i = i + 1;
                }
            }
        }

        void computeLPSArray(string pat, int M, int[] lps)
        {
            // length of the previous longest prefix suffix 
            int len = 0;
            int i = 1;
            lps[0] = 0; // lps[0] is always 0 

            // the loop calculates lps[i] for i = 1 to M-1 
            while (i < M)
            {
                if (pat[i] == pat[len])
                {
                    len++;
                    lps[i] = len;
                    i++;
                }
                else // (pat[i] != pat[len]) 
                {
                    // This is tricky. Consider the example. 
                    // AAACAAAA and i = 7. The idea is similar 
                    // to search step. 
                    if (len != 0)
                    {
                        len = lps[len - 1];

                        // Also, note that we do not increment 
                        // i here 
                    }
                    else // if (len == 0) 
                    {
                        lps[i] = len;
                        i++;
                    }
                }
            }
        }

    }
}
