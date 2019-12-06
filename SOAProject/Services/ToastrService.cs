using SOAProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOAProject
{
    public static class ToastrService
    {
        private static readonly List<(DateTime Date, string SessionId, Toastr Toastr)> toastrs
            = new List<(DateTime Date, string SessionId, Toastr Toastr)>();

        private static string GetSessionId()
        {
            return BaseObject.Session;
        }

        public static void AddToUserQueue(Toastr toastr)
        {
            string sessionId = BaseObject.Session;
            toastrs.Add((Date: DateTime.Now, SessionId: GetSessionId(), Toastr: toastr));
        }

        public static void AddToUserQueue(string message, string title, ToastrType type)
        {
            AddToUserQueue(new Toastr()
            {
                Message = message,
                Title = title,
                Type = type,
            });

        }

        public static bool HasUserQueue()
        {
            string sessionId = GetSessionId();
            return toastrs.Any(x => x.SessionId == sessionId);
        }

        public static void RemoveQueue()
        {
            string sessionId = GetSessionId();
            toastrs.RemoveAll(x => x.SessionId == sessionId);
        }

        public static void ClearAll()
        {
            toastrs.Clear();
        }

        public static List<(DateTime Date, string SessionId, Toastr Toastr)> ReadAndUserQueue()
        {
            string sessionId = GetSessionId();
            return toastrs.Where(x => x.SessionId == sessionId).OrderBy(x => x.Date).ToList();
        }

        public static List<(DateTime Date, string SessionId, Toastr Toastr)> ReadAndRemoveUserQueue()
        {
            var list = ReadAndUserQueue();
            RemoveQueue();
            return list;
        }
    }
}