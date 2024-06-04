using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Channel
{
  public  class CategoryManager
    {

     
      static List<Group> _groups;
      public CategoryManager()
      {
        
      }

      List<Group> groups { get {
          if (_groups== null)
          {
              ChannelBotEntities _channelBotEntities = new ChannelBotEntities();
              _groups = new List<Group>();
              _groups = _channelBotEntities.Groups.ToList();
          }
          return _groups;
      } }

      public Group GetCategoryByName(string categoryName)
      {
          return groups.FirstOrDefault(g => g.Name == categoryName);
      }


      internal List<Group> GetGroups()
      {
          return groups;
      }

 
    }
}
