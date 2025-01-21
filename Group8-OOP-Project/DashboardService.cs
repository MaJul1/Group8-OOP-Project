using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Group8_OOP_Project
{
    internal class DashboardService
    {
        private MySqlService service = new MySqlService();

        public async Task LoadUsersToListView(ListView listView)
        {
            listView.View = View.Details;

            listView.Columns.Add("ID");
            listView.Columns.Add("Name");
            listView.Columns.Add("Age");
            listView.Columns.Add("Address");

            var listViewItems = await GetUserListViewItems();

            listView.Items.AddRange(listViewItems.ToArray());

            foreach (ColumnHeader column in listView.Columns)
            {
                column.Width = -2;
            }
        }

        private async Task<List<ListViewItem>> GetUserListViewItems()
        {
            var listViewItems = new List<ListViewItem>();

            var users = await service.GetAllUser();
            foreach (var user in users)
            {
                var item = new ListViewItem(user.Id.ToString());
                item.SubItems.Add(user.Name);
                item.SubItems.Add(user.Age.ToString());
                item.SubItems.Add(user.Address);
                listViewItems.Add(item);
            }

            return listViewItems;
        }
    }
}
