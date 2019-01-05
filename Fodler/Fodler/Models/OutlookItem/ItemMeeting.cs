using Accord.MachineLearning;
using Fodler.Helpers;
using Microsoft.Office.Interop.Outlook;

namespace Fodler.Models.OutlookItem
{
    public class ItemMeeting : IOutlookItem
    {
        private readonly MeetingItem _item;
        public ItemMeeting(MeetingItem item)
        {
            _item = item;
        }

        private Folder GetParent()
        {
            return _item.Parent;
        }

        public string[] GetInput()
        {
            return new[]
                {
                    "meeting",
                    _item.SenderEmailAddress ?? "",
                    _item.SenderName ?? "",
                    OutlookHelpers.GetDomainFromEmail(_item.SenderEmailAddress)
                };
        }

        public string GetStoreId()
        {
            return GetParent().StoreID;
        }

        public string[] GetSubject()
        {
            return _item.Subject?.Tokenize() ?? new[] { "" };
            //return OutlookItemHelpers.GetKeywords(_item.Subject?.Tokenize() ?? new[] { "" }, 3).ToArray();
        }

        public string[] GetText()
        {
            return OutlookItemHelpers.GetKeywords(_item.Body, 5, 1).ToArray();
        }

        public void Move(Folder folder)
        {
            if (!GetParent().Name.Equals(folder.Name))
            {
                _item.Move(folder);
            }
        }
    }
}
