using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkHttp
{
    ///<summary>
    ///description:database type
    ///</summary>
    enum DataBase
    {
        sqlserver,
        sqlite
    }

    class httpresponseType
    {
        ///<summary>
        ///description:this method create a download link for file.
        ///</summary>
        public static void link(Http.HttpResponse resp,string filename,string foldername)
        {
            resp.Header.Add("Content-Disposition", "attachment; filename=\"" + filename + "\"");
            resp.RawContent = mimetypestring.readfile(filename,foldername);
        }

        ///<summary>
        ///description:this method load jquery.min.js into js folder
        ///</summary>
        public static void jqueryminjs(Http.HttpResponse resp)
        {
            resp.ContentType = "text/javascript";
           System.IO.StreamReader rd = new System.IO.StreamReader(@"..\..\js\jquery.min.js");
           string jq = rd.ReadToEnd();
           resp.Content = jq; 
        }

        ///<summary>
        ///description:this method return javascript file string stream
        ///</summary>
        public static string Readjs(string jsfile)
        {
            System.IO.StreamReader rd = new System.IO.StreamReader(@"..\..\js\"+jsfile);
            string js = rd.ReadToEnd();
            return js;
        }

        ///<summary>
        ///description:this method load javascript file into js folder for http response
        ///</summary>
        public static void loadjs(Http.HttpResponse resp, string jsfile)
        {
            resp.ContentType = "text/javascript";
            resp.RawContent = System.IO.File.ReadAllBytes(@"..\..\js\"+jsfile);
            
        }

        ///<summary>
        ///description:this method load bootstrap.min.js into js folder
        ///</summary>
        public static void bootstrapminjs(Http.HttpResponse resp)
        {
            resp.ContentType = "text/javascript";
            System.IO.StreamReader rd = new System.IO.StreamReader(@"..\..\js\bootstrap.min.js");
            string bootjs = rd.ReadToEnd();
            resp.Content = bootjs; 
        }

        ///<summary>
        ///description:this method load css file into css folder.
        ///</summary>
        public static void loadcss(Http.HttpResponse resp, string filename)
        {
            resp.ContentType = "text/css";
            System.IO.StreamReader rd = new System.IO.StreamReader(@"..\..\css\"+filename);
            string bc = rd.ReadToEnd();
            resp.Content = bc;
        }

        ///<summary>
        ///description:this method load html page in the default pages folder directory.
        ///</summary>
        public static void htmlpage(Http.HttpResponse resp,string pagename,string appendjs)
        {
            System.IO.StreamReader rd = new System.IO.StreamReader(@"..\..\pages\" + pagename);
            string htmpage = rd.ReadToEnd();
            resp.Content = htmpage.Replace("</body>",appendjs+"</body>"); 
        }

        ///<summary>
        ///description:this method load html page in the default pages folder directory.
        ///</summary>
        public static void htmlpage(Http.HttpResponse resp,string pagename,string appendcontent,string elemtag)
        {
            System.IO.StreamReader rd = new System.IO.StreamReader(@"..\..\pages\" + pagename);
            string htmpage = rd.ReadToEnd();
            resp.Content = htmpage.Replace(elemtag,elemtag+appendcontent); 
        }
        ///<summary>
        ///description:this method load a html page.
        ///</summary>
        public static void htmlpage(Http.HttpResponse resp, string pagename,string foldername, string appendcontent, string elemtag)
        {
            System.IO.StreamReader rd = new System.IO.StreamReader(@"..\..\" + foldername + @"\" + pagename);
            string htmpage = rd.ReadToEnd();
            resp.Content = htmpage.Replace(elemtag,elemtag + appendcontent);
        }

        ///<summary>
        ///description:load a video or picture into Specified folder.
        ///</summary>
        public static void loadpicvideo(Http.HttpResponse resp, string filename,string foldername)
        {
            resp.ContentType = "image/JPEG";
            resp.RawContent = System.IO.File.ReadAllBytes(@"..\..\"+foldername+@"\" + filename);
        }

        ///<summary>
        ///description:load a swf sock wave flash file.
        ///</summary>
        public static void loadswf(Http.HttpResponse resp, string filename, string foldername)
        {
            resp.ContentType = "application/x-shockwave-flash";
            resp.RawContent = System.IO.File.ReadAllBytes(@"..\..\" + foldername + @"\" + filename);
        }

        public static void EXNONquery(Http.HttpResponse resp, DataBase dbType, string constr, string sqlcmd)
        {
            switch (dbType)
            {
                case DataBase.sqlite:
                    System.Data.SQLite.SQLiteConnection sqlitecnn = new System.Data.SQLite.SQLiteConnection(constr);
                    sqlitecnn.Open();
                    try { UseSqlite.DBAction.ExecuteNQcmd(sqlcmd, sqlitecnn); }
                    catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
                    sqlitecnn.Close();
                    resp.Content = "ok data set to database";
                    break;
            }
        }
        public static string readDBfiled(DataBase dbType,string filedname)
        {
            string[] dblist=new string[100];
            string dboutall = null;
            switch (dbType)
            {
                case DataBase.sqlite:
                    if (filedname == "name")
                    {
                        try
                        {
                            var dbr = new classes.CourseraContext();
                            var dbout = dbr.Courses.Where(n => n.Id==1).Select(n => n.Name);
                            dblist = dbout.ToArray();
                        }
                        catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.Message); }
                    }
                    foreach (string st in dblist)
                        dboutall += st ;
                    break;
            }
            return dboutall;
        }
        ///<summary>
        ///description:get form Action url address.
        ///</summary>
        public static string form_postto(Http.HttpRequest req)
        {
            string[] url_split = {"?"};
            return req.Url.Split(url_split, StringSplitOptions.None)[0];
        }

        public static void defaultresponse(Http.HttpRequest req,Http.HttpResponse resp)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<html><head><script src=\"js/jquery.min.js\"></script><link href=\"css/bootstrap.min.css\" rel=\"stylesheet\"></head><body>");
            sb.Append("<h3>Session</h3>");
            sb.Append("<p>ID: " + req.Session.ID + "<br>User: " + req.Session.User);
            sb.Append("<h3>Header</h3>");
            sb.Append("Method: " + req.Method + "; URL: '" + req.Url + "'; HTTP version " + req.HttpVersion + "<p>");
            foreach (KeyValuePair<string, string> ide in req.Header) sb.Append(" " + ide.Key + ": " + ide.Value + "<br>");
            sb.Append("<h3>Cookies</h3>");
            foreach (KeyValuePair<string, string> ide in req.Cookies) sb.Append(" " + ide.Key + ": " + ide.Value + "<br>");
            sb.Append("<h3>Query</h3>");
            sb.Append(req.QueryString+"<br/>");
            foreach (KeyValuePair<string, string> ide in req.Query)
            {
                sb.Append(" " + ide.Key + ": " + ide.Value + "<br>");
                if (ide.Key == "imgdata")
                {
                   // mimetypestring.savecontent(ide.Value, "data:image/jpeg;base64,", "clientimage.jpg");
                }
            }
            sb.Append("<h3>Content</h3>");
            sb.Append(req.Content);
            sb.Append("<a href='/testpage'>testpage</a>");
            sb.Append("<button type=\"button\" class=\"btn btn-primary col-lg-offset-5\" id=\"but\">show modal</button>");
            sb.Append("<script>$('#but').click(function(){$('p').hide()})</script>");
            sb.Append("</body></html>");
            resp.Content = sb.ToString();
        }

    }
}
