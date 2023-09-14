namespace Senthil.WAE.Assignment
{
    using Senthil.WAE.Assignment.Model;
    using Senthil.WAE.Assignment.Utilities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;

    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblStatusMessage.Text = string.Empty;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ProcessButton_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            var filteredCollection = new List<Formula>();
            Helper.Operators filterCondition = (Helper.Operators)Enum.Parse(typeof(Helper.Operators), DdlOperators.SelectedValue);
            string outputFileName = LblChannel.Text + " " + txtChannel.Text.Trim() + "_" + filterCondition.ToString() + "_" + LblValue.Text + "_" + txtValue.Text +"_"+ DateTime.Now.ToString("ddMMyyyyHHmmss");
            try
            {
                var fileContent = new List<Formula>();
                var fileColumn = new List<string[]>();

                if (InputFileUpload.HasFile)
                {
                    using (StreamReader inputStreamReader = new StreamReader(InputFileUpload.PostedFile.InputStream))
                    {
                        int rowCount = 0;
                        string line;
                        while ((line = inputStreamReader.ReadLine()) != null)
                        {
                            var fields = line.Split('|');
                            if (rowCount == 0)
                            {
                                fileColumn.Add(fields);
                            }
                            else
                            {
                                var formula = new Formula()
                                {
                                    TimeSpan = TimeSpan.Parse(fields[0].Trim()),
                                    Value = Convert.ToDecimal(fields[1].Trim()),
                                    Channel = fields[2].Trim()
                                };
                                fileContent.Add(formula);
                            }
                            rowCount++;
                        }
                    }

                    List<Filter> filter = new List<Filter>()
                            {
                                new Filter { PropertyName = LblChannel.Text, Operation = Helper.Operators.Equal, Value = LblChannel.Text.ToLower() + " " + txtChannel.Text },
                                new Filter { PropertyName = LblValue.Text, Operation = filterCondition, Value = Convert.ToDecimal(txtValue.Text.Trim())  },
                            };

                    var query = ExpressionBuilder.GetExpression<Formula>(filter).Compile();
                    filteredCollection = fileContent.Where(query).ToList();

                    sb.Append(fileColumn.ToComma(x => x[0] + "|" + x[1] + "|" + x[2]));
                    sb.Append("\r\n");
                    sb.Append(filteredCollection.ToComma(x => x.TimeSpan + "|" + x.Value + "|" + x.Channel));
                }
                else
                {
                    lblStatusMessage.Text = "You did not select a file to process.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblStatusMessage.Text = ex.Message;
            }

            try
            {
                if (filteredCollection.Capacity > 0)
                {
                    Response.Clear();
                    Response.ClearHeaders();
                    Response.AppendHeader("Content-Length", sb.Length.ToString());
                    Response.ContentType = "text/plain";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=\"" + outputFileName + ".dat\"");
                    Response.Buffer = true;
                    Response.Write(sb);
                    Response.Flush();
                }
                else
                {
                    lblStatusMessage.Text = "There is no data filtered from the file.";
                    return;
                }
            }
            catch (Exception ex)
            {
                lblStatusMessage.Text = ex.Message;
            }
            finally
            {
                if (filteredCollection.Capacity > 0)
                    Response.End();
            }
        }
    }
}