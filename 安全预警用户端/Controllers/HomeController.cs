using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.HtmlControls;
using 安全预警用户端.Models;

namespace SensorUser.Controllers
{
    public class HomeController : Controller
    {
        private SysInfoEntities db = new SysInfoEntities();
        private static string opo = "";
        List<string> list = new List<string>();
        public ActionResult Index()
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Syssz> users = db.Syssz.ToList();
            ViewBag.u = users[0].Tite.Trim();
            return View();
        }
        //public static List<Models.socketMod> Listws = new List<socketMod>();

        //public object SessionHelper { get; private set; }

        //private async Task Websockets(AspNetWebSocketContext arg)
        //{
        //    var web = arg.WebSocket;
        //    while (true)
        //    {
        //        //ArraySegment数组的小抽屉，用于对该数组中元素的范围进行分隔
        //        ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
        //        //开始接收
        //        WebSocketReceiveResult result = await web.ReceiveAsync(buffer, CancellationToken.None);
        //        //判断通信状态是否是打开的
        //        if (web.State == WebSocketState.Open)
        //        {
        //            string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);
        //            //这里可以自己自定义，我当时是用于前端可以做出停止指令来操作控制后台的任务，所以这样写
        //            if (message.Contains("停止"))
        //            {
        //                var id = message.Split('|').LastOrDefault()?.ObjTolong();
        //                var mesd = Listws.FirstOrDefault(p => p.userid == id);
        //                mesd.isstop = true;
        //            }
        //            //这里是接收前端发来的消息，然后做判断的,其中socketMod是自定义的一个实体类，用来存储与客户端连接的信息，比如后台消息要发送到前台时要通过唯一id来在此实体类数组里查找，然后进行指定发送到哪个客户端上，这个可以用来存放在CallContext（线程里唯一）里，这样可以做成简单的聊天器。
        //            if (message.ObjTolong() > 0 && !Listws.Exists(p => p.userid == message.ObjTolong()))
        //            {
        //                var md = new Models.socketMod();
        //                md.SecWebSocketKey = arg.SecWebSocketKey;
        //                md.userid = message.ObjTolong();
        //                md.webst = web;
        //                Listws.Add(md);
        //            }
        //            var mes = new BLL.mescontent();
        //            mes.jdt = "0";
        //            mes.mess = "成功连接 :" + DateTime.Now.ToLongTimeString();
        //            buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(mes.MToString()));
        //            //发送消息到前台，这里可以通过调用Listws实体类数组来指定发送或群发
        //            await web.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        //        }
        //        else
        //        {
        //            if (Listws.Exists(p => p.SecWebSocketKey == arg.SecWebSocketKey))
        //            {
        //                Listws.Remove(Listws.FirstOrDefault(p => p.SecWebSocketKey.Equals(arg.SecWebSocketKey)));
        //                SessionHelper.SetCach("websockect", Listws);
        //            }
        //            break;
        //        }
        //    }
        //}
        
        public ActionResult About()
        {

            db.Configuration.ProxyCreationEnabled = false;
            List<Syssz> users = db.Syssz.ToList();
            ViewBag.uiu = users[0].Tite.Trim();
            List<SensorInfo> sensorinfos = db.SensorInfo.ToList();
            int len = sensorinfos.Count;
            int[] senid = new int[len];

            ViewBag.len = len;
            for (int i = 0; i < len; i++)
            {
                senid[i] = sensorinfos[i].SenID;
            }
            ViewBag.n = opo;
            //Json(senid, JsonRequestBehavior.AllowGet);
            ViewBag.senid = JsonConvert.SerializeObject(senid);
            return View();
        }

        string o = "";
        [HttpPost]
        public ActionResult Contact1(string da, string da2, string ide)
        {
            string a = ide;
            int x = int.Parse(da);
            int y = int.Parse(da2);
            adress adr = db.adress.Find(a);
            adr.X = x;
            adr.Y = y;
            db.SaveChanges();
            string io = Request["a"];
            
            list.Add(a);
            list.Add(x.ToString());
            list.Add(y.ToString());

            TempData["Message"] = JsonConvert.SerializeObject(list);
            return Json(io);
        }
        [HttpPost]
        public ActionResult Contact2(string da, string da2, string ide)
        {
            int a = int.Parse(ide);
            int x = int.Parse(da);
            int y = int.Parse(da2);
            adress adr = db.adress.Find(a);
            adr.X = x;
            adr.Y = y;
            db.SaveChanges();
            string io = Request["a"];
            return Json(io);
        }
        public ActionResult Contact()
        {
            List<Syssz> syssz = db.Syssz.ToList();
            ViewBag.img =  syssz[0].pic.ToString().Trim();
            string uu= Request["gender"];
            List<adress> ad = db.adress.ToList();
            int len1 = ad.Count;
            int[] senid1 = new int[len1];
            var obj = new
            {
                title = ad[0].SenID.Trim(),
                positionX = ad[0].X,
                positionY = ad[0].Y
            };
            New_Soy newd = new New_Soy();

            string str = JsonConvert.SerializeObject(obj);
            ViewBag.idde = str;
            for (int i = 0; i < len1; i++)
            {
                string a1 = "'"+ad[i].SenID.Trim()+"'";
                ViewBag.iddt = ad[i].SenID;
                string b = "'b'";
                int aaa = 1;
                //{ title: '52', positionX: 100, positionY: 120 }
                uu += "{title:" + a1.Trim() + ",positionX:" + ad[i].X + ",positionY:" + ad[i].Y + "}," ;
                //uu += string.Format("{title:{0},title2:'{1}',positionX:,positionY:},{title:,positionX:,positionY:},",a);

                //var obj = new
                //{
                //    title = a1,
                //    positionX = ad[i].X,
                //    positionY = ad[i].Y
                //};
            }
            string a = "["+uu+"]";
            ViewBag.idd = a;


            List<SensorInfo> sensorinfos = db.SensorInfo.ToList();
            int len = sensorinfos.Count;
            int[] senid = new int[len];

            ViewBag.len = len;
            for (int i = 0; i < len; i++)
            {
                senid[i] = sensorinfos[i].SenID;
            }
            ViewBag.n = opo;
            //Json(senid, JsonRequestBehavior.AllowGet);
            ViewBag.senid = JsonConvert.SerializeObject(senid);
            //o= Request["UserName"].ToString();
            return View();
        }
       
        public ActionResult Login(string UserName, string UserPwd)
        {
            var strMsg = "";
            var strUser = UserName;
            opo = strUser;
            var strPwd = UserPwd;
            //try
            //{
            var dbAdmin = (from d in db.User where d.UserName.Trim() == strUser.Trim() select d).Single();
            var md5 = new MD5CryptoServiceProvider();
            strPwd = BitConverter.ToString(md5.ComputeHash(Encoding.Default.GetBytes(strPwd)), 4, 8);
            if (dbAdmin.UserPwd.Trim().Equals(strPwd))
            {
                strMsg = "success";
            }
            else
            {
                strMsg = "PassErro";
            }
            //}
            //catch (Exception e)
            //{

            //    strMsg = "usernoexsit";
            //    Console.WriteLine("e");
            //}

            return Json(strMsg, JsonRequestBehavior.AllowGet);

        }
        public ActionResult Soy()
        {

            return View();


        }

        public ActionResult GetSoyList1(string timestar = "1990-1-1 0:0:0.000", string timestop = "2019-10-25 10:46:11.000", string UserName = "", string IP = "", int page = 1, int rows = 10,
    string sort = "SoyID", string order = "desc")
        {
            //DateTime dttr = DateTime.Now;
            //DateTime dttr = DateTime.Now;
            DateTime  timestar1 = DateTime.Parse(timestar);
            DateTime timestop1 = DateTime.Parse(timestop);

            IQueryable<SoY> soy = (from d in db.SoY where d.SoYTime >= timestar1 && d.SoYTime <= timestop1 select d).AsQueryable();

            
            if (string.IsNullOrWhiteSpace(UserName) && string.IsNullOrWhiteSpace(IP))
            {
                soy = (from d in db.SoY where d.SoYTime >= timestar1 && d.SoYTime <= timestop1 select d).AsQueryable();
                //soy = db.SoY.AsQueryable();
            }
            else
            {
                if (string.IsNullOrWhiteSpace(UserName))
                {

                    soy = db.SoY.Where(u => u.SenIP.ToString().Contains(IP) && u.SoYTime >= timestar1 && u.SoYTime <= timestop1).AsQueryable();
                }
                else if (string.IsNullOrWhiteSpace(IP))
                {
                    soy = db.SoY.Where(u => u.SenZt.ToString().Contains(UserName) && u.SoYTime >= timestar1 && u.SoYTime <= timestop1).AsQueryable();
                }
                else
                {
                    soy = db.SoY.Where(u => u.SenZt.ToString().Contains(UserName) && u.SenIP.ToString().Contains(IP) && u.SoYTime >= timestar1 && u.SoYTime <= timestop1).AsQueryable();
                }
            }

            //paixu
            if (order == "desc")
            {
                switch (sort)
                {
                    case "SoyID":
                        soy = soy.OrderByDescending(u => u.SoyID);
                        break;
                    case "SenID":
                        soy = soy.OrderByDescending(u => u.SenID);
                        break;
                    case "SenIP":
                        soy = soy.OrderByDescending(u => u.SenIP);
                        break;
                    case "SenZt":
                        soy = soy.OrderByDescending(u => u.SenZt);
                        break;
                    case "SoYTime":
                        soy = soy.OrderByDescending(u => u.SoYTime);
                        break;
                    default:
                        soy = soy.OrderByDescending(u => u.UserID);
                        break;
                }
            }
            else
            {

                switch (sort)
                {
                    case "SoyID":
                        soy = soy.OrderBy(u => u.SoyID);
                        break;
                    case "SenID":
                        soy = soy.OrderBy(u => u.SenID);
                        break;
                    case "SenIP":
                        soy = soy.OrderBy(u => u.SenIP);
                        break;
                    case "SenZt":
                        soy = soy.OrderBy(u => u.SenZt);
                        break;
                    case "SoYTime":
                        soy = soy.OrderBy(u => u.SoYTime);
                        break;
                    default:
                        soy = soy.OrderBy(u => u.UserID);
                        break;
                }
            }

            //分页
            int total = soy.Count();
            if (total > 0)
            {
                if (page <= 1)
                {
                    soy = soy.Take(rows);
                }
                else
                {
                    soy = soy.Skip((page - 1) * rows).Take(rows);
                }
            }
            ///zhushi
            db.Configuration.ProxyCreationEnabled = false;

            var obj = new
            {
                total = total,
                rows = soy.ToArray()
            };
            return Json(obj, JsonRequestBehavior.AllowGet);



        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}