using Accord.MachineLearning;
using Fodler.Helpers;
using Microsoft.Office.Interop.Outlook;

namespace Fodler.Models.OutlookItem
{
    public class ItemMail : IOutlookItem
    {
        private readonly MailItem _item;
        public ItemMail(MailItem item)
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
                    "mail",
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
