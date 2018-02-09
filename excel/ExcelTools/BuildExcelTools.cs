using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using LitJson;
using OfficeOpenXml;

namespace ExcelTools
{
    public partial class BuildExcelTools : Form
    {
        private List<FileStream> _excelList = new List<FileStream>();
        private Dictionary<string, ExcelWorksheet> _selectWorkList = new Dictionary<string, ExcelWorksheet>();
        private Dictionary<string, ExcelWorksheet> _sourceWorkList = new Dictionary<string, ExcelWorksheet>();

        private SerializableDictionary<string, string> _userBuildDataDic = new SerializableDictionary<string, string>();

        private string userDataPath = Application.CommonAppDataPath + "/";
        private const string userDataFileName = "buildExcelData.xml";

        private string _excelPath = "";
        private string _csFilePath = "";
        private string _dataFilePath = "";

        private ListBox _ctgListBox;

        public enum FieldRule { RULE_ERROR = 0, RULE_COMMON, RULE_SERVER, RULE_CLIENT, RULE_IGNORE }

        private static Dictionary<string, FieldRule> color_rule = new Dictionary<string, FieldRule>()
        {
            {"FF00B0F0", FieldRule.RULE_COMMON},
            {"FF00B050", FieldRule.RULE_CLIENT},
            {"FFFFC000", FieldRule.RULE_SERVER},
        };

        public FieldRule get_color_rule(string color)
        {
            try
            {
                return color_rule[color];
            }
            catch (Exception e)
            {
                return FieldRule.RULE_SERVER;
            }
        }

        public BuildExcelTools()
        {
            InitializeComponent();
            searchSetTxt.TextChanged += SelectSearchTxtOnTextChanged;
            searchPreTxt.TextChanged += SourceSearchTxtOnTextChanged;
            allSet_btn.Click += AllSetOnClick;
            clearAllSet_btn.Click += ClearAllSetOnClick;
            addSet_btn.Click += AddSetOnClick;
            removeSet_btn.Click += RemoveSetOnClick;
            selectListBox.SelectedIndexChanged += LisboxSelectChanged;
            previewListBox.SelectedIndexChanged += LisboxSelectChanged;
            selectListBox.GotFocus += SelectListBoxOnGotFocus;
            previewListBox.GotFocus += SelectListBoxOnGotFocus;
            buildExcel_btn.Click += BuildButtonHandle;
            createCs_btn.Click += BuildButtonHandle;
            build_create_btn.Click += BuildButtonHandle;
            clearContext_btn.Click += clearContext_btn_Click;
            csFile_btn.Click += CsFileBtnOnClick;
            dataFile_btn.Click += DataFileBtnOnClick;
            reset_btn.Click += reset_btn_Click;
            SelectListBoxOnGotFocus(null, null);
            InitData();
        }

        private void InitData()
        {
            if (!Directory.Exists(userDataPath))
                Directory.CreateDirectory(userDataPath);
            if (File.Exists(userDataPath + userDataFileName))
            {
                try
                {
                    FileStream fileStream = new FileStream(userDataPath + userDataFileName,FileMode.Open,FileAccess.Read,FileShare.ReadWrite);
                    XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<string, string>));
                    _userBuildDataDic = (SerializableDictionary<string, string>)xmlFormatter.Deserialize(fileStream);
                    fileStream.Close();
                    fileStream.Dispose();
                    if (_userBuildDataDic.ContainsKey("excelPath"))
                    {
                        _excelPath = _userBuildDataDic["excelPath"];
                        if (!string.IsNullOrEmpty(_excelPath))
                            excelPath.Text = _excelPath;
                    }
                    if (_userBuildDataDic.ContainsKey("csFilePath"))
                    {
                        _csFilePath = _userBuildDataDic["csFilePath"];
                        if (!string.IsNullOrEmpty(_csFilePath))
                            csFileLabel.Text = _csFilePath;
                    }
                    if (_userBuildDataDic.ContainsKey("dataFilePath"))
                    {
                        _dataFilePath = _userBuildDataDic["dataFilePath"];
                        if (!string.IsNullOrEmpty(_dataFilePath))
                            dataFileLabel.Text = _dataFilePath;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("读取缓存文件失败!");
                    throw;
                }
            }
            if (!string.IsNullOrEmpty(_excelPath))
            {
                InitFileList();
            }
        }

        private void UpdateCacheFile()
        {
            if (File.Exists(userDataPath + userDataFileName))
                File.Delete(userDataPath + userDataFileName);
            try
            {
                FileStream fileStream = new FileStream(userDataPath + userDataFileName, FileMode.OpenOrCreate);
                XmlSerializer xmlFormatter = new XmlSerializer(typeof(SerializableDictionary<string, string>));
                xmlFormatter.Serialize(fileStream, _userBuildDataDic);
                fileStream.Close();
                fileStream.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("存储缓存文件失败!");
                throw;
            }
        }

        private void DataFileBtnOnClick(object sender, EventArgs eventArgs)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(browserDialog.SelectedPath))
                {
                    if (MessageBox.Show("请选择正确的文件路径!", "确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        DataFileBtnOnClick(sender, eventArgs);
                    }
                    return;
                }
                _dataFilePath = browserDialog.SelectedPath + "/";
                dataFileLabel.Text = _dataFilePath;
                if (_userBuildDataDic.ContainsKey("dataFilePath"))
                    _userBuildDataDic["dataFilePath"] = _dataFilePath;
                else
                    _userBuildDataDic.Add("dataFilePath", _dataFilePath);
                UpdateCacheFile();
            }
        }

        private void CsFileBtnOnClick(object sender, EventArgs eventArgs)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(browserDialog.SelectedPath))
                {
                    if (MessageBox.Show("请选择正确的文件路径!", "确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        CsFileBtnOnClick(sender, eventArgs);
                    }
                    return;
                }
                _csFilePath = browserDialog.SelectedPath + "/";
                csFileLabel.Text = _csFilePath;
                if (_userBuildDataDic.ContainsKey("csFilePath"))
                    _userBuildDataDic["csFilePath"] = _csFilePath;
                else
                    _userBuildDataDic.Add("csFilePath", _csFilePath);
                UpdateCacheFile();
            }
        }

        private void SelectListBoxOnGotFocus(object sender, EventArgs eventArgs)
        {
            if (sender == null)
            {
                LisboxSelectChanged(null, null);
                return;
            }
            ListBox listBox = sender as ListBox;
            if (listBox == previewListBox)
                selectListBox.ClearSelected();
            if (listBox == selectListBox)
                previewListBox.ClearSelected();
        }


        private void LisboxSelectChanged(object sender, EventArgs eventArgs)
        {
            addSet_btn.Enabled = false;
            removeSet_btn.Enabled = false;
            if (sender == null) return;
            ListBox listBox = sender as ListBox;
            _ctgListBox = listBox;
            if (listBox.SelectedItems.Count > 0)
            {
                addSet_btn.Enabled = listBox == previewListBox;
                removeSet_btn.Enabled = listBox == selectListBox;
            }
        }

        //获取本地excel目录
        private void BrowserFolderPanel(object sender, EventArgs e)
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();
            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                if (string.IsNullOrEmpty(browserDialog.SelectedPath))
                {
                    if (MessageBox.Show("请选择正确的Excel文件路径!", "确定", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        BrowserFolderPanel(sender, e);
                    }
                    return;
                }
                _excelPath = browserDialog.SelectedPath + "/";
                excelPath.Text = _excelPath;
                if (_userBuildDataDic.ContainsKey("excelPath"))
                    _userBuildDataDic["excelPath"] = _excelPath;
                else
                    _userBuildDataDic.Add("excelPath", _excelPath);
                UpdateCacheFile();
                InitFileList();
            }
        }

        private void InitFileList()
        {
            if (Directory.Exists(_excelPath))
            {
                _excelList.Clear();
                FileInfo[] fileInfos = new DirectoryInfo(_excelPath).GetFiles("*.xlsx", SearchOption.AllDirectories);
                foreach(var fileinfo in fileInfos)
                {
                    if (fileinfo.Name.IndexOf("~$") == -1)
                    {
                        string filePath = fileinfo.DirectoryName + "/" + fileinfo.Name;
                        FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                        _excelList.Add(fileStream);
                    }
                }
            }
            if (_excelList.Count >= 1)
            {
                _sourceWorkList.Clear();
                foreach (var fileInfo in _excelList)
                {
                    try
                    {
                        ExcelPackage excelPackage = new ExcelPackage(fileInfo);
                        ExcelWorksheets excelWorksheets = excelPackage.Workbook.Worksheets;
                        foreach (var excelWorksheet in excelWorksheets)
                        {
                            if (excelWorksheet.Name.IndexOf("Unusedtxt", StringComparison.Ordinal) < 0 &&
                                excelWorksheet.Name.IndexOf("Sheet", StringComparison.Ordinal) < 0
                                && excelWorksheet.Name.IndexOf("S_", 0, 2, StringComparison.Ordinal) < 0 &&
                                !_sourceWorkList.ContainsKey(excelWorksheet.Name))
                            {
                                Console.Out.WriteLine("sheetName = " + excelWorksheet.Name + "  fileName = " +
                                                      fileInfo.Name);
                                _sourceWorkList.Add(excelWorksheet.Name, excelWorksheet);
                            }

                        }
                    }
                    catch (Exception err)
                    {
                        MessageBox.Show("读取excel文件出错! Exception : " + err);
                        throw;
                    }
                }
            }
            UpdateListBox();
        }


        private void ShowListBoxByList(ListBox listbox, List<ExcelWorksheet> sheetList, bool isAdd = true)
        {
            listbox.BeginUpdate();
            listbox.Items.Clear();
            if (sheetList != null)
            {
                foreach (var worksheet in sheetList)
                {
                    if (isAdd)
                        listbox.Items.Add(worksheet.Name);
                    else
                        listbox.Items.Remove(worksheet.Name);
                }
            }
            listbox.EndUpdate();
        }

        private List<ExcelWorksheet> SearchExcel(List<ExcelWorksheet> workList, string _searchTxt)
        {
            if (string.IsNullOrEmpty(_searchTxt)) return null;
            List<ExcelWorksheet> searchList = new List<ExcelWorksheet>();
            foreach (var workSheet in workList)
            {
                if (workSheet.Name.ToLower().Contains(_searchTxt.ToLower()))
                    searchList.Add(workSheet);
            }
            return searchList;
        }

        private void UpdateListBox()
        {
            previewListBox.ClearSelected();
            selectListBox.ClearSelected();
            ShowListBoxByList(previewListBox, _sourceWorkList.Values.ToList());
            ShowListBoxByList(selectListBox, _selectWorkList.Values.ToList());
        }

        private void SelectSearchTxtOnTextChanged(object sender, EventArgs eventArgs)
        {
            var list = searchSetTxt.Text.Length >= 1 ? SearchExcel(_selectWorkList.Values.ToList(), searchSetTxt.Text) : _selectWorkList.Values.ToList();
            ShowListBoxByList(selectListBox, list);
        }
        private void SourceSearchTxtOnTextChanged(object sender, EventArgs eventArgs)
        {
            var list = searchPreTxt.Text.Length >= 1 ? SearchExcel(_sourceWorkList.Values.ToList(), searchPreTxt.Text) : _sourceWorkList.Values.ToList();
            ShowListBoxByList(previewListBox, list);
        }

        private void BuildButtonHandle(object sender, EventArgs e)
        {
            bool isBuild = false;
            if (sender == build_create_btn) //转换并生成
            {
                //isBuild = BuildExcelData();
                //if (!isBuild) return;
                //isBuild = CreateCSFile();
                //if (!isBuild) return;
                isBuild = BuildAllLuaFile();
                if (!isBuild) return;
                isBuild = BuildAllExcelErlFile();
                if (!isBuild) return;
            }
            if (sender == createCs_btn)  //生成cs文件
            {
                //isBuild = CreateCSFile();
                isBuild = BuildAllLuaFile();
                if (!isBuild) return;
            }
            else if (sender == buildExcel_btn)      //转换数据表文件
            {
                isBuild = BuildAllExcelErlFile();
                if (!isBuild) return;
            }
            MessageBox.Show("BuildExcel 完成!!!!!");
        }


        private bool BuildExcelData()
        {
            if (string.IsNullOrEmpty(_dataFilePath))
            {
                MessageBox.Show("请先选择你导出数据的文件夹!");
                return false;
            }
            if (_selectWorkList.Count == 0)
            {
                MessageBox.Show("请至少选择一个需要转换的表!");
                return false;
            }
            foreach (var worksheet in _selectWorkList.Values)
            {
                Dictionary<string, Dictionary<string, string>> _binaryDic = new Dictionary<string, Dictionary<string, string>>();
                int Rows = worksheet.Dimension.Rows;
                int Columns = worksheet.Dimension.Columns;
                for (int rows = 5; rows <= Rows; rows++)
                {
                    if (!string.IsNullOrEmpty(worksheet.GetValue<string>(rows, 2))) //主键值不为空
                    {
                        Dictionary<string, string> sructDic = new Dictionary<string, string>();
                        for (int cloums = 2; cloums <= Columns; cloums++)
                        {
                            if (worksheet.GetValue<string>(1, cloums) == "required")
                            {
                                string key = worksheet.GetValue<string>(3, cloums);
                                string value = worksheet.GetValue<string>(rows, cloums);
                                if (worksheet.GetValue<string>(4, cloums) == "PRIMARY_KEY")
                                    sructDic.Add("unikey", value);
                                if (!sructDic.ContainsKey(key))
                                    sructDic.Add(key, value);
                                else
                                    if (MessageBox.Show("出现重复的字段,表名->" + worksheet.Name + " 重复字段 ->" + key, "确定", MessageBoxButtons.OK) == DialogResult.OK)
                                        return false;
                            }
                        }
                        if (sructDic.ContainsKey("unikey"))
                        {
                            if (!_binaryDic.ContainsKey(sructDic["unikey"]))
                                _binaryDic.Add(sructDic["unikey"], sructDic);
                            else
                                if (MessageBox.Show("出现重复的主键值,表名->" + worksheet.Name + " 主键值->" + sructDic["unikey"], "确定", MessageBoxButtons.OK) == DialogResult.OK)
                                    return false;
                        }
                        else
                        {
                            if (MessageBox.Show("没有设置主键值,表名->" + worksheet.Name, "确定", MessageBoxButtons.OK) == DialogResult.OK)
                                return false;
                        }
                    }

                }
                string fileName = worksheet.Name + ".bytes";
                UpdateBuildState("转表 " + fileName + " 成功!");
                BeginChangeTobinary(_binaryDic, fileName);
            }
            return true;
        }
        private bool CreateCSFile()
        {
            if (string.IsNullOrEmpty(_csFilePath))
            {
                MessageBox.Show("请先选择你导出Lua文件的文件夹!");
                return false;
            }
            if (_selectWorkList.Count == 0)
            {
                MessageBox.Show("请至少选择一个需要生成的表!");
                return false;
            }
            UpdateBuildState("创建ExcelVo文件!");
            if (!Directory.Exists(_csFilePath))
                Directory.CreateDirectory(_csFilePath);
            foreach (var worksheet in _selectWorkList)
            {
                string sheetKey = worksheet.Key.Substring(0, 1).ToUpper() +
                                  worksheet.Key.Substring(1, worksheet.Key.Length - 1);
                string fileName = sheetKey + ".cs";
                FileStream fileStream = new FileStream(_csFilePath + fileName, FileMode.Create);
                StreamWriter sw = new StreamWriter(fileStream, Encoding.UTF8);
                sw.Write(CreateUsing());
                sw.WriteLine("namespace com.game.data\n{");
                sw.Write(CreateStruct(sheetKey, worksheet.Value));
                sw.WriteLine("}");
                UpdateBuildState("生成" + fileName + "文件成功!");
                sw.Close();
                sw.Dispose();
                fileStream.Close();
                fileStream.Dispose();
            }
            return true;
        }

        private StringBuilder CreateUsing()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            return sb;
        }
        private StringBuilder CreateStruct(string structName, ExcelWorksheet worksheet)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("\t[Serializable]");
            sb.AppendLine("\tpublic class " + structName + "\n\t{");

            int colunms = worksheet.Dimension.Columns;
            string varValue = "\t\tpublic {0} {1} {2} get; set; {3} //{4}";
            List<string> unikeyList = new List<string>();
            sb.AppendLine(string.Format(varValue, "string", "unikey", "{", "}", "主键 -> 客户端使用"));
            for (int i = 1; i <= colunms; i++)
            {
                string title = worksheet.GetValue<string>(1, i);
                Console.Out.WriteLine("title = " + title);
                if (title == "required")
                {
                    string value = worksheet.GetValue<string>(3, i);
                    if (!unikeyList.Contains(value))
                    {
                        unikeyList.Add(value);
                        string var = worksheet.GetValue<string>(2, i);
                        string desc = worksheet.GetValue<string>(4, i);
                        if (var == "int8" || var == "int16" || var == "int32" ||
                            var == "int64")
                            var = "int";
                        else if (var == "list" || var == "dict" || var == "List<int>" || var == "List<string>")
                            var = "string";
                        sb.AppendLine(string.Format(varValue, var, value, "{", "}", desc));
                    }
                }
            }
            sb.AppendLine("\t}");
            return sb;
        }


        private void BeginChangeTobinary(object binaryDic, string fileName)
        {
            var jsondata = JsonMapper.ToJson(binaryDic);
            if (!Directory.Exists(_dataFilePath))
                Directory.CreateDirectory(_dataFilePath);
            UpdateBuildState("开始生成 " + fileName + "文件!  路径 = " + (_dataFilePath + fileName));
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            try
            {
                FileStream fileStream = new FileStream(_dataFilePath + fileName, FileMode.Create);
                binaryFormatter.Serialize(fileStream, jsondata);
                fileStream.Close();
                fileStream.Dispose();
            }
            catch (Exception)
            {
                MessageBox.Show("生成  " + fileName + "文件出错!");
                throw;
            }
            UpdateBuildState("生成  " + fileName + "文件成功!  路径 = " + (_dataFilePath + fileName));
        }

        private void ReadToBinary()
        {
            string filePath = _dataFilePath + "chapterData.bytes";
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (FileStream fileStream = File.OpenRead(filePath))
            {
                IDictionary dic = JsonMapper.ToObject(binaryFormatter.Deserialize(fileStream).ToString());
                fileStream.Close();
                fileStream.Dispose();
            }
        }

        bool BuildAllLuaFile()
        {
            //if (string.IsNullOrEmpty(_dataFilePath))
            //{
            //    MessageBox.Show("请先选择你导出数据的文件夹!");
            //    return false;
            //}
            if (_selectWorkList.Count == 0)
            {
                MessageBox.Show("请至少选择一个需要转换的表!");
                return false;
            }
            if (_selectWorkList.Count == 0)
            {
                MessageBox.Show("请至少选择一个需要转换的表!");
                return false;
            }
            foreach (var worksheet in _selectWorkList.Values)
            {
                BuildLuaFile(worksheet);
            }
            return true;
        }
        /*
            erl 文件
         */
        private bool BuildAllExcelErlFile()
        {
            if (string.IsNullOrEmpty(_dataFilePath))
            {
                MessageBox.Show("请先选择你导出数据的文件夹!");
                return false;
            }
            if (_selectWorkList.Count == 0)
            {
                MessageBox.Show("请至少选择一个需要转换的表!");
                return false;
            }
            foreach (var worksheet in _selectWorkList.Values)
            {
                if (worksheet.Name == "ecode")
                {
                    BuildErrorCodeErlFile(worksheet);
                }
                else
                {
                    BuildErlFile(worksheet);
                }      
            }
            return true;
        }

        private bool BuildLuaFile(ExcelWorksheet worksheet)
        {
            string strFileName = worksheet.Name;
            strFileName = "Cfg" + strFileName;
            string strFilePathName = _csFilePath + strFileName + ".lua";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("--[[--------------------------------------------------------------");
            sb.AppendLine("");
            string strNameEx = "%%%" + strFileName + "(自动生成，请勿修改！)";
            sb.AppendLine(strNameEx);
            sb.AppendLine();
            sb.AppendLine("----------------------------------------------------------------]]");
            sb.AppendLine();
            sb.AppendLine("local t = {}");
            sb.AppendLine();
            int Rows = worksheet.Dimension.Rows;
            int Columns = worksheet.Dimension.Columns;
            int primarykeyCol = 2;
            for (int i = 0; i <= Columns; i++)
            {
                if (worksheet.GetValue<string>(4, i) == "PRIMARY_KEY")
                {
                    primarykeyCol = i;
                    break;
                }
            }
            HashSet<string> keySet = new HashSet<string>();
            for (int rows = 5; rows <= Rows; rows++)
            {
                var primarykey = worksheet.GetValue<string>(rows, primarykeyCol);
                string keyValue = primarykey;
                if (primarykey == null || string.IsNullOrEmpty(primarykey) || string.IsNullOrWhiteSpace(primarykey))
                    continue;
                if(keySet.Contains(primarykey))
                {
                    if (MessageBox.Show("出现重复的字段,表名->" + worksheet.Name + " 重复字段 ->" + primarykey, "确定", MessageBoxButtons.OK) == DialogResult.OK)
                        return false;
                }
                keySet.Add(primarykey);
                sb.Append(string.Format("t[{0}] = {{", FormatLuaValue(primarykey)));
                if (!string.IsNullOrEmpty(worksheet.GetValue<string>(rows, 2))) //主键值不为空
                {
                    for (int cloums = 2; cloums <= Columns; cloums++)
                    {
                        if (cloums == primarykeyCol)
                            continue;
                        if (worksheet.GetValue<string>(1, cloums) == "required")
                        {
                            ExcelRange range = worksheet.Cells[1, cloums];
                            string rgb = range.Style.Fill.BackgroundColor.Rgb;
                            if (get_color_rule(rgb) != FieldRule.RULE_COMMON && get_color_rule(rgb) != FieldRule.RULE_CLIENT)
                            {
                                continue;
                            }

                            string key = worksheet.GetValue<string>(3, cloums);
                            string value = worksheet.GetValue<string>(rows, cloums);
                            value = FormatLuaValue(value);
                            if(value != null)
                            {
                                string valueType = worksheet.GetValue<string>(2, cloums);
                                sb.Append(string.Format("{0} = ", key));
                                sb.Append(FormatLuaValue(value));
                                if (cloums < Columns)
                                    sb.Append(", ");
                            }
                        }
                    }
                    sb.Append("}");
                    sb.AppendLine();
                }
            }

            sb.AppendLine();
            sb.Append("return t");
            File.WriteAllBytes(strFilePathName, Encoding.UTF8.GetBytes(sb.ToString()));
            return true;
        }

        string FormatLuaValue(string value)
        {
            if (string.IsNullOrEmpty(value))
                return null;
            double res;
            if (double.TryParse(value, out res))
                return value;
            value = value.TrimStart(' ', '\t', '\n');
            value = value.TrimEnd(' ', '\t', '\n');
            value = value.Replace("\n", "");
            if (value[0] == '{' && value.EndsWith("}"))
                return value;
            if (value[0] != '\"')
                value = "\"" + value;
            if(!value.EndsWith("\""))
                value = value +  "\"";
            return value;
        }

        /*
            
         */
        private bool BuildErlFile(ExcelWorksheet worksheet)
        {
            string strFileName = worksheet.Name;
            strFileName = "sys_" + strFileName.ToLower();
            string strFilePathName = _dataFilePath + strFileName + ".erl";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("%%%----------------------------------------------------------------");
            sb.AppendLine("%%%");
            string strNameEx = "%%%" + strFileName.ToLower() + "(自动生成，请勿修改！)";
            sb.AppendLine(strNameEx);
            sb.AppendLine("%%%");
            sb.AppendLine("%%%----------------------------------------------------------------");
            sb.AppendFormat("-module({0}).\r\n",strFileName);
            sb.AppendLine("-include(\"wg_log.hrl\").");
            sb.AppendLine("-export([get/1, list/0]).");
            string strAllList = "";
            int Rows = worksheet.Dimension.Rows;
            int Columns = worksheet.Dimension.Columns;
                for (int rows = 5; rows <= Rows; rows++)
                {
                    if (!string.IsNullOrEmpty(worksheet.GetValue<string>(rows, 2))) //主键值不为空
                    {
                        bool hasValue = false;
                        Dictionary<string, string> sructDic = new Dictionary<string, string>();
                        for (int cloums = 2; cloums <= Columns; cloums++)
                        {
                            if (worksheet.GetValue<string>(1, cloums) == "required")
                            {
                                ExcelRange range = worksheet.Cells[1, cloums];
                                string rgb = range.Style.Fill.BackgroundColor.Rgb;
                                if (get_color_rule(rgb) != FieldRule.RULE_COMMON && get_color_rule(rgb) != FieldRule.RULE_SERVER)
                                {
                                    continue;
                                }
                                string key = worksheet.GetValue<string>(3, cloums);
                                string value = worksheet.GetValue<string>(rows, cloums);
                                string valueType = worksheet.GetValue<string>(2, cloums);
                                if(valueType == "string")
                                {
                                    value = quote_str(value);
                                }
                                if (worksheet.GetValue<string>(4, cloums) == "PRIMARY_KEY" && value != "")
                                {
                                    sb.Append("get(" + value + ")-> {" + strFileName);
                                    strAllList = strAllList == "" ? strAllList + value : strAllList + "," + value;
                                }
                                if (!sructDic.ContainsKey(key))
                                {
                                    if(value == "")
                                    {
                                        value = "none";
                                    }
                                    else if(value == null)
                                    {
                                        value = "none";
                                    }
                                    sb.Append("," + value);
                                }
                                else
                                {
                                    if (MessageBox.Show("出现重复的字段,表名->" + worksheet.Name + " 重复字段 ->" + key, "确定", MessageBoxButtons.OK) == DialogResult.OK)
                                        return false;
                                }
                            }
                        }
                        sb.AppendLine(" };");
                    }

                }
            sb.AppendLine("");
            sb.AppendLine("get(_Id) -> ?ERROR(\"data not exist:~p\", [_Id]), throw({error, 20}). ");
            sb.AppendFormat("list() -> [{0}].\r\n", strAllList);
            File.WriteAllBytes( strFilePathName, Encoding.UTF8.GetBytes(sb.ToString()) );
            return true;
        }
        private bool BuildErrorCodeErlFile(ExcelWorksheet worksheet)
        {
            string strFileName = worksheet.Name;
            strFileName = "sys_" + strFileName.ToLower();
            string strFilePathName = _dataFilePath + strFileName + ".hrl";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("%%%----------------------------------------------------------------");
            sb.AppendLine("%%%");
            string strNameEx = "%%%" + strFileName.ToLower() + "(自动生成，请勿修改！)";
            sb.AppendLine(strNameEx);
            sb.AppendLine("%%%");
            sb.AppendLine("%%%----------------------------------------------------------------");
            int Rows = worksheet.Dimension.Rows;
            int Columns = worksheet.Dimension.Columns;
            for (int rows = 5; rows <= Rows; rows++)
            {
                if (!string.IsNullOrEmpty(worksheet.GetValue<string>(rows, 2))) //主键值不为空
                {
                    Dictionary<string, string> sructDic = new Dictionary<string, string>();
                    string strkeyValue = worksheet.GetValue<string>(rows, 2);
                    string strkeyDefine = worksheet.GetValue<string>(rows, 3);
                    string strkeyDesc = worksheet.GetValue<string>(rows, 4);
                    sb.AppendLine("-define(" + strkeyDefine + ", " + strkeyValue + "). %" + strkeyDesc);
                }

            }
            File.WriteAllBytes(strFilePathName, Encoding.UTF8.GetBytes(sb.ToString()));
            return true;
        }

        private string quote_str(string str)
        {
            if(str.Length == 0)
                return "\"\"";
            double res;
            if(double.TryParse(str, out res))
                return str;
            if(!str.StartsWith("\""))
            str = '"' + str;
            if(!str.EndsWith("\""))
                str = str + '"';
            return str;
        }

        private void RemoveSetOnClick(object sender, EventArgs eventArgs)
        {
            var selectItems = selectListBox.SelectedItems;
            foreach (var item in selectItems)
            {
                string itemKey = item.ToString();
                if (_selectWorkList.ContainsKey(itemKey))
                {
                    _sourceWorkList.Add(itemKey, _selectWorkList[itemKey]);
                    _selectWorkList.Remove(itemKey);
                }
            }
            UpdateListBox();
        }

        private void AddSetOnClick(object sender, EventArgs eventArgs)
        {
            var selectItems = previewListBox.SelectedItems;
            foreach (var item in selectItems)
            {
                string itemKey = item.ToString();
                if (_sourceWorkList.ContainsKey(itemKey))
                {
                    _selectWorkList.Add(itemKey, _sourceWorkList[itemKey]);
                    _sourceWorkList.Remove(itemKey);
                }
            }
            UpdateListBox();
        }

        private void ClearAllSetOnClick(object sender, EventArgs eventArgs)
        {
            if (_ctgListBox != null)
                _ctgListBox.ClearSelected();
        }

        private void AllSetOnClick(object sender, EventArgs eventArgs)
        {
            if (_ctgListBox != null)
            {
                var items = _ctgListBox.Items;
                for (int i = 0; i < items.Count; i++)
                {
                    _ctgListBox.SetSelected(i, true);
                }
            }
        }

        private void UpdateBuildState(string context)
        {
            buildStateBox.BeginUpdate();
            buildStateBox.Items.Add(context);
            buildStateBox.EndUpdate();
        }

        private void clearContext_btn_Click(object sender, EventArgs e)
        {
            buildStateBox.BeginUpdate();
            buildStateBox.Items.Clear();
            buildStateBox.EndUpdate();
        }

        private void reset_btn_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
