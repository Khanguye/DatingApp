using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.SignalR
{
    public class PresenceTracker
    {
        //Dictionary is not safe thread. it needs to be lock when update
        private static readonly Dictionary<string, List<string>> OnlineUsers = new Dictionary<string, List<string>>();


        public Task<bool> UserConnected(string username, string conntectionId){
            
            bool isOnline = false;
            lock(OnlineUsers){

                if (OnlineUsers.ContainsKey(username)){
                    OnlineUsers[username].Add(conntectionId);
                }
                else{
                    OnlineUsers.Add(username, new List<string>{conntectionId});
                    isOnline=true;
                }

            }

            return Task.FromResult(isOnline);
        }

        public Task<bool> UserDisconnected(string username, string conntectionId){
               bool isOffline = false;
               lock(OnlineUsers){
                   if (!OnlineUsers.ContainsKey(username)) return Task.FromResult(isOffline);

                    OnlineUsers[username].Remove(conntectionId);
                    if (OnlineUsers[username].Count == 0)
                    {
                        OnlineUsers.Remove(username);
                        isOffline = true;
                    }
                   
               }
               return Task.FromResult(isOffline);  
        }

        public Task<string[]> GetOnlineUsers(){
               string[] onlineUsers;
               lock(OnlineUsers){
                onlineUsers = OnlineUsers.OrderBy(d=> d.Key).Select(d=> d.Key).ToArray();
               }

               return Task.FromResult(onlineUsers);
        }

        public Task<List<string>> GetConnectionsForUser(string username){
            List<string> connectionIds;
            lock(OnlineUsers){
                connectionIds = OnlineUsers.GetValueOrDefault(username);
            }

            return Task.FromResult(connectionIds);
        }
    }
}
