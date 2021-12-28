using Application.ViewModels;

namespace Application.Services
{
    public static class EnumToListService
    {
        public static List<EnumToListViewModel> EnumToList(object test)
        {
            List<EnumToListViewModel> list = new();
            foreach (object foo in Enum.GetValues(test.GetType()))
            {
                list.Add(new EnumToListViewModel() { Key = (int)foo, Value = foo.ToString() });
            }
            return list;
        }
    }
}
