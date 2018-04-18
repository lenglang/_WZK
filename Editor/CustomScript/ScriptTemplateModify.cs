//using System.Collections;
//using System.IO;
//using System.Text;
//using System.Text.RegularExpressions;
//using IniParser.Model;
//using IniParser.Model.Configuration;
//using IniParser.Parser;
//using ServiceStack.Text;
//using UnityEditor;
//using UnityEngine;

//namespace Babybus.Uno
//{
//    public class ScriptTemplateModify : UnityEditor.AssetModificationProcessor
//    {
//        private static readonly string AuthorCode =
//        "//=======================================================\r\n"
//        + "// 作者：#UserName#\r\n"
//        + "// 描述：\r\n"
//        + "//=======================================================\r\n";

//        public static readonly string headCode =
//        "using UnityEngine;\r\n"
//        + "using System.Collections;\r\n"
//        + "\r\n";

//        public static void OnWillCreateAsset(string path)
//        {
//            //只修改C#脚本
//            var scriptName = "";
//            path = path.Replace(".meta", "");
//            if (path.EndsWith(".cs"))
//            {
//                string identifierNamespace = SearchForIdentifierNamespace(path);
//                string allText = "";
//                allText += File.ReadAllText(path);
//                scriptName = GetClassName(allText);
//                if (scriptName != "")
//                {
//                    CreateClass(path, scriptName, identifierNamespace);
//                    AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);
//                }
//            }
//        }

//        public static void CreateClass(string path, string className, string ns)
//        {
//            var sb = new ScriptBuilder();
//            if (string.IsNullOrEmpty(ns) && UnoCfg.App.IsCodeNameValid())
//                ns = "Babybus." + FirstLetterUppercase(UnoCfg.App.CodeName());
//            if (string.IsNullOrEmpty(ns))
//                ns = "Babybus.Project";

//            sb.WriteLine("namespace " + ns);
//            sb.WriteCurlyBrackets();
//            sb.Indent++;
//            sb.WriteLine("public class #SCRIPTNAME# : MonoBehaviour");
//            sb.WriteCurlyBrackets();
//            sb.Indent++;

//            var allText = AuthorCode + headCode + sb.ToString();
//            //替换脚本名字
//            allText = allText.Replace("#SCRIPTNAME#", className);
//            //替换作者名字
//            allText = allText.Replace("#UserName#", GetUserName());

//            File.WriteAllText(path, allText);
//        }

//        public static string GetUserName()
//        {
//            var path = "";
//            if (Application.platform == RuntimePlatform.WindowsEditor)
//                path = System.Environment.GetEnvironmentVariable("USERPROFILE");
//            else
//                path = "/users/" + System.Environment.UserName;
//            path = PathUtil.Build(path, ".gitconfig");
//            var parser = new IniParser.FileIniDataParser();
//            if (!File.Exists(path))
//                return "";
//            var ini = parser.ReadFile(path, Encoding.UTF8);
//            return ini["user"]["name"];
//        }

//        public static string FirstLetterUppercase(string str)
//        {
//            if (string.IsNullOrEmpty(str))
//                return str;
//            if (str.Length == 1)
//                return str.ToUpper();

//            var first = str[0];
//            var rest = str.Substring(1);
//            return first.ToString().ToUpper() + rest;
//        }

//        public static string GetClassName(string allText)
//        {
//            var patterm = "public class ([A-Za-z0-9_]+)\\s*:\\s*MonoBehaviour {\\s*\\/\\/ Use this for initialization\\s*void Start \\(\\) {\\s*}\\s*\\/\\/ Update is called once per frame\\s*void Update \\(\\) {\\s*}\\s*}";
//            var match = Regex.Match(allText, patterm);
//            if (match.Success)
//            {
//                return match.Groups[1].Value;
//            }
//            return "";
//        }

//        private static string SearchForIdentifierNamespace(string path)
//        {
//            var dir = Path.GetDirectoryName(path);
//            string identifierFile = SearchForIdentifierNamespaceRecursively(dir);
//            if (string.IsNullOrEmpty(identifierFile))
//                return null;

//            var parser = new IniDataParser();
//            IniData data;
//            try
//            {
//                data = parser.Parse(File.ReadAllText(identifierFile));
//            }
//            catch (System.Exception ex)
//            {
//                Debug.LogException(ex);
//                data = null;
//            }
//            if (data != null)
//                return data.Global["namespace"];

//            return null;
//        }
//        private static string SearchForIdentifierNamespaceRecursively(string dir)
//        {
//            if (string.IsNullOrEmpty(dir))
//                return null;

//            var files = Directory.GetFiles(dir, "__*Identifier__.txt");
//            if (files.Length > 0)
//                return files[0];
//            return SearchForIdentifierNamespaceRecursively(Path.GetDirectoryName(dir));
//        }
//    }
//}