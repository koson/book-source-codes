using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mediator.Core
{
    public class Participant : IParticipant
    {
        private IChatRoom chatroom;

        public string Name { get; set; }

        public Participant(string name, IChatRoom chatroom)
        {
            this.Name = name;
            this.chatroom = chatroom;
        }

        public void Send(string to, string message)
        {
            chatroom.Send(this.Name, to, message);
        }

        public void Receive(string from, string message)
        {
            ChatMessage msg = new ChatMessage();
            msg.From = from;
            msg.To = this.Name;
            msg.Message = message;
            msg.SentOn = DateTime.Now;
            using (AppDbContext db = new AppDbContext())
            {
                db.ChatMessages.Add(msg);
                db.SaveChanges();
            }
        }

        public List<ChatMessage> GetChatHistory()
        {
            using (AppDbContext db = new AppDbContext())
            {
                var query = from m in db.ChatMessages
                            where m.To == Name || m.From == Name
                            orderby m.SentOn ascending
                            select m;
                return query.ToList();
            }
        }

    }

}
