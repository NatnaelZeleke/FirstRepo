using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Antlr.Runtime.Misc;
using Domain.Infrastructure;
using Domain.Services;
using Web.ViewModels;

namespace Web.Services
{
    public class Converter:IConverter
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        public Converter(IUserService userService,IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
        }
        public TenderType TenderTypeViewModelToTenderType(TenderTypeViewModel tenderTypeViewModel)
        {
            return new TenderType
            {
                Id = tenderTypeViewModel.Id,
                Name = tenderTypeViewModel.Name,
                NameAmharic = tenderTypeViewModel.NameAmharic
            };
        }

        public IEnumerable<TenderTypeViewModel> TenderTypeToTenderTypeViewModel(IEnumerable<TenderType> tendetTypes)
        {
            var list = new List<TenderTypeViewModel>();
            if (tendetTypes == null) return list;
            list.AddRange(tendetTypes.Select(TenderTypeToTenderTypeViewModel));
            return list;
        }

        public TenderTypeViewModel TenderTypeToTenderTypeViewModel(TenderType tenderType)
        {

            return new TenderTypeViewModel
            {
                Id = tenderType.Id,
                Name = tenderType.Name,
                NameAmharic = tenderType.NameAmharic
            };
        }

        public IEnumerable<TenderViewModel> TenderToTenderViewModels(IEnumerable<Tender> tenders,long userId)
        {
            var list = new List<TenderViewModel>();
            if (tenders == null) return list;
//            list.AddRange(tenders.Select(TenderToTenderViewModels));
            list.AddRange(from t in tenders select TenderToTenderViewModels(t,userId));
            return list;
        }

        public TenderViewModel TenderToTenderViewModels(Tender tender,long userId)
        {
            if (tender == null) return null;
            return new TenderViewModel
            {
                Id = tender.Id,
                TenderTypeId = tender.TenderTypeId,
                OpeningDay = tender.OpeningDay,
                Address = tender.Address,
                PostedOn = tender.PostedOn,
                TenderTitle = tender.TenderTitle,
                ClosingDay = tender.ClosingDay,                
                Description = tender.Description,                               
                TenderType = TenderTypeToTenderTypeViewModel(tender.TenderType),
                NewsPaper = NewsPaperToNewsPaperViewModel(tender.NewsPaper),
                NewsPaperId = tender.NewsPaperId,
                NewsPaperPublishDate = tender.NewsPaperPublishDate,
                IsSavedByUser = IsSavedByUser(tender,userId),
                IsOpen = tender.ClosingDay == null || DateTime.Now < tender.ClosingDay
            };
        }

        private bool IsSavedByUser(Tender tender,long userId)
        {
            if (!tender.UserSavedTenders.Any()) return false;
            var tenderSaves = tender.UserSavedTenders?.Where(ut => ut.UserId == userId);
            return tenderSaves.Any();
        }

        public Tender TenderViewModelToTender(TenderViewModel tenderViewModel)
        {
            return new Tender
            {
                Id = tenderViewModel.Id,
                TenderTypeId = tenderViewModel.TenderTypeId,
                OpeningDay = tenderViewModel.OpeningDay,
                Address = tenderViewModel.Address,
                PostedOn = tenderViewModel.PostedOn,
                TenderTitle = tenderViewModel.TenderTitle,
                Description = tenderViewModel.Description,
                ClosingDay = tenderViewModel.ClosingDay,
                NewsPaperId = tenderViewModel.NewsPaperId,
                NewsPaperPublishDate = tenderViewModel.NewsPaperPublishDate
            };
        }

        public UserTenderTypeChoice SelectedTenderTypeIdsToUserTenderTypeChoice(long userId, long[] selectedTenderTypeIds)
        {
            return new UserTenderTypeChoice
            {
                Id = userId,
                TenderTypeIds = string.Join(",",selectedTenderTypeIds.ToArray())
            };
        }

        public IEnumerable<TenderTypeViewModel> TenderTypeToTenderTypeViewModel(long[] sttId, IEnumerable<TenderType> tenderTypes)
        {
            var list = new List<TenderTypeViewModel>();
            if (tenderTypes == null) return list;

//            this was chnaged to the below linq query 
//            foreach (var tt in tenderTypes)
//            {
//                var selected = sttId.Contains(tt.Id);
//                list.Add(TenderTypesToTenderTypeViewModel(selected, tt));
//            }

            list.AddRange(from tt in tenderTypes let selected = sttId.Contains(tt.Id) select TenderTypeToTenderTypeViewModel(selected, tt));
            return list;
        }

        public TenderTypeViewModel TenderTypeToTenderTypeViewModel(bool selected, TenderType tenderType)
        {
            if (tenderType == null) return null;
            return new TenderTypeViewModel
            {
                Id = tenderType.Id,
                Name = tenderType.Name,
                NameAmharic = tenderType.NameAmharic,
                Selected = selected
            };
        }

        public IEnumerable<NewsPaperViewModel> NewsPaperToNewsPaperViewModel(long[] snpId, IEnumerable<NewsPaper> newsPapers)
        {
            var list = new List<NewsPaperViewModel>();
            list.AddRange(from np in newsPapers let selected = snpId.Contains(np.Id) select NewsPaperToNewsPaperViewModel(selected,np));
            return list;
        }

        public NewsPaperViewModel NewsPaperToNewsPaperViewModel(bool selected, NewsPaper newsPaper)
        {
            if (newsPaper == null) return null;
            return new NewsPaperViewModel
            {
                Id = newsPaper.Id,
                Name = newsPaper.Name,
                NameAmharic = newsPaper.NameAmharic,
                Selected = selected
            };
        }

        public IEnumerable<NewsPaperViewModel> NewsPaperToNewsPaperViewModel(IEnumerable<NewsPaper> newsPapers)
        {
            var list = new List<NewsPaperViewModel>();
            if (newsPapers == null) return list;
            list.AddRange(newsPapers.Select(NewsPaperToNewsPaperViewModel));
            return list;
        }

        public NewsPaperViewModel NewsPaperToNewsPaperViewModel(NewsPaper newsPaper)
        {
            return new NewsPaperViewModel
            {
                Id  = newsPaper.Id,
                Name = newsPaper.Name,
                NameAmharic = newsPaper.NameAmharic
            };
        }

        public long[] UserTenderTypeIdsToArray(string userTenderTypeIds)
        {
            return userTenderTypeIds.Split(',').Select(long.Parse).ToArray();
        }

        public string UserTenderTypeIdsToString(long[] userTenderTypeIds)
        {
            return string.Join(",", userTenderTypeIds);
        }

        public UserSavedTender SaveTenderViewModelToUserSaveTender(SaveTenderViewModel saveTenderViewModel)
        {
            return new UserSavedTender
            {
                UserId = saveTenderViewModel.UserId,
                TenderId = saveTenderViewModel.TenderId
            };
        }

        public IEnumerable<TenderViewModel> SavedTenderToTenderViewModel(IEnumerable<UserSavedTender> userSavedTender)
        {
            var list = new List<TenderViewModel>();
            if (userSavedTender == null) return list;
            list.AddRange(from ust in userSavedTender select TenderToTenderViewModels(ust.Tender,ust.UserId));
            return list;
        }

        public IEnumerable<UserViewModel> UserToUserViewModel(IEnumerable<User> user)
        {
            var list  = new List<UserViewModel>();
            if (user == null) return list;
            list.AddRange(user.Select(UserToUserViewModel));
             
            return list;
        }

        public RegisteredUserViewModel UserToRegisteredUserViewModel(IEnumerable<User> user)
        {
            var model = new RegisteredUserViewModel
            {
                ActiveUserAccountViewModel = new List<UserAccountViewModel>(),
                InActiveUserAccountViewModel = new ListStack<UserAccountViewModel>()
            };
            foreach (var u in user)
            {
                var userAccount = _accountService.GetAccountById(u.Id);
                if (userAccount.AccountExpirationDate > DateTime.Now)
                {
                    //active user
                    model.ActiveUserAccountViewModel.Add(UserToUserAccountViewModel(u));
                }
                else
                {
                    //inactive user
                    model.InActiveUserAccountViewModel.Add(UserToUserAccountViewModel(u));
                }
            }
            return model;
        }

        private UserAccountViewModel UserToUserAccountViewModel(User user)
        {
            return new UserAccountViewModel
            {
                User = UserToUserViewModel(user),
                Account = AccountToAccountViewModel(user.Account)
            };

        }
        public UserViewModel UserToUserViewModel(User user)
        {
            return new UserViewModel { Id = user.Id, UserName = user.UserName, Email = user.Email,PhoneNumber = user.PhoneNumber};
        }

        public AccountViewModel AccountToAccountViewModel(Account account)
        {
            return new AccountViewModel
            {
                Id = account.Id,
                RegisteredOn = account.RegisteredOn,
                AccountExpirationDate = account.AccountExpirationDate
            };
        }

        public IEnumerable<NewsPaperViewModel> TenderReportToNewsPaperViewModel(IEnumerable<IEnumerable<Tender>> groupedTenders)
        {
            var list = new List<NewsPaperViewModel>();
            if (groupedTenders == null) return list;
            list.AddRange(groupedTenders.Select(TenderReportToNewsPaperViewModel));
            return list;
        }

        public NewsPaperViewModel TenderReportToNewsPaperViewModel(IEnumerable<Tender> groupedTender)
        {
            if (groupedTender == null) return null;
            var model = groupedTender.First().NewsPaper;
            return new NewsPaperViewModel
            {
                Id = model.Id,
                Name = model.Name,
                NameAmharic = model.NameAmharic,
                Count = groupedTender.Count()
            };
        }

        public IEnumerable<TenderTypeViewModel> TenderToTenderTypeViewModel(IEnumerable<IEnumerable<Tender>> groupedTenders)
        {
            var list = new List<TenderTypeViewModel>();
            if (groupedTenders == null) return list;
            list.AddRange(groupedTenders.Select(TenderToTenderTypeViewModel));
            return list;
        }

        public TenderTypeViewModel TenderToTenderTypeViewModel(IEnumerable<Tender> groupedTender)
        {
            if (groupedTender == null) return null;
            var model = groupedTender.First().TenderType;
            return new TenderTypeViewModel
            {
                Id = model.Id,
                Name = model.Name,
                NameAmharic = model.NameAmharic,
                Count = groupedTender.Count()
            };
        }

        public UsersBySale UsersBySalesViewModelToUserBySale(UsersBySalesViewModel userBySalesVm)
        {
            return new UsersBySale
            {
                UserId = userBySalesVm.UserId,
                FullName = userBySalesVm.FullName,
                PhoneNumber = userBySalesVm.PhoneNumber,
                Email = userBySalesVm.Email
            };
        }

        public IEnumerable<UsersBySalesViewModel> UsersBySalesToUsersBySalesViewModel(IEnumerable<UsersBySale> usersBySales)
        {
            var list = new List<UsersBySalesViewModel>();
            if (usersBySales == null) return list;
            list.AddRange(usersBySales.Select(UserBySalesToUsersBySalesViewModel));
            return list;
        }

        public UsersBySalesViewModel UserBySalesToUsersBySalesViewModel(UsersBySale usersBySale)
        {
            //check if the user is registered or not
            var registered = false;
            var paid = false;
            if (usersBySale.PhoneNumber != null)
            {
                registered = _userService.GetUserByPhoneNumber(usersBySale.PhoneNumber) != null;
            }

            if (registered)
            {
                var userAccount = _userService.GetUserByPhoneNumber(usersBySale.PhoneNumber).Account;
                paid = userAccount.AccountExpirationDate > DateTime.Now;
            }
             


            return new UsersBySalesViewModel
            {
                FullName = usersBySale.FullName,
                PhoneNumber = usersBySale.PhoneNumber,
                Email = usersBySale.Email,
                Registered = registered,
                Paid = paid
            };
        }

        public IEnumerable<SalesManViewModel> UsersToSalesViewModel(IEnumerable<User> users)
        {
            var list = new List<SalesManViewModel>();
            if (users == null) return list;
            list.AddRange(users.Select(UserToSalesViewModel));
            return list;
        }

        public SalesManViewModel UserToSalesViewModel(User user)
        {

            return new SalesManViewModel
            {
                User = UserToUserViewModel(user),
                CustomersCount = user.UsersBySales.Count
            };
        }
    }

    public interface IConverter
    {
        TenderType TenderTypeViewModelToTenderType(TenderTypeViewModel tenderTypeViewMoel);
        IEnumerable<TenderTypeViewModel> TenderTypeToTenderTypeViewModel(IEnumerable<TenderType> tendetTypes);
        TenderTypeViewModel TenderTypeToTenderTypeViewModel(TenderType tenderType);
        IEnumerable<TenderViewModel> TenderToTenderViewModels(IEnumerable<Tender> tenders,long userId);
        TenderViewModel TenderToTenderViewModels(Tender tender,long userId);
        Tender TenderViewModelToTender(TenderViewModel tenderViewModel);
        UserTenderTypeChoice SelectedTenderTypeIdsToUserTenderTypeChoice(long userId, long[] selectedTenderTypeIds);

        IEnumerable<TenderTypeViewModel> TenderTypeToTenderTypeViewModel(long[] sttId, IEnumerable<TenderType> tenderTypes);

        TenderTypeViewModel TenderTypeToTenderTypeViewModel(bool selected, TenderType tenderType);

        IEnumerable<NewsPaperViewModel> NewsPaperToNewsPaperViewModel(long[] snpId, IEnumerable<NewsPaper> newsPapers);
        NewsPaperViewModel NewsPaperToNewsPaperViewModel(bool selected, NewsPaper newsPaper);
        IEnumerable<NewsPaperViewModel> NewsPaperToNewsPaperViewModel(IEnumerable<NewsPaper> newsPapers);
        NewsPaperViewModel NewsPaperToNewsPaperViewModel(NewsPaper newsPaper);

        long[] UserTenderTypeIdsToArray(string userTenderTypeIds);
        string UserTenderTypeIdsToString(long[] userTenderTypeIds);

        UserSavedTender SaveTenderViewModelToUserSaveTender(SaveTenderViewModel saveTenderViewModel);

        IEnumerable<TenderViewModel> SavedTenderToTenderViewModel(IEnumerable<UserSavedTender> userSavedTender);

        

        AccountViewModel AccountToAccountViewModel(Account account);

        IEnumerable<NewsPaperViewModel> TenderReportToNewsPaperViewModel(
            IEnumerable<IEnumerable<Tender>> groupedTenders);

        

        NewsPaperViewModel TenderReportToNewsPaperViewModel(IEnumerable<Tender> groupedTender);
        IEnumerable<TenderTypeViewModel> TenderToTenderTypeViewModel(IEnumerable<IEnumerable<Tender>> tenderType);
        TenderTypeViewModel TenderToTenderTypeViewModel(IEnumerable<Tender> tenderType);

        UsersBySale UsersBySalesViewModelToUserBySale(UsersBySalesViewModel userBySalesVm);

        IEnumerable<UsersBySalesViewModel> UsersBySalesToUsersBySalesViewModel(IEnumerable<UsersBySale> usersBySales);
        UsersBySalesViewModel UserBySalesToUsersBySalesViewModel(UsersBySale usersBySale);

        IEnumerable<SalesManViewModel> UsersToSalesViewModel(IEnumerable<User> users);

        SalesManViewModel UserToSalesViewModel(User user);

        IEnumerable<UserViewModel> UserToUserViewModel(IEnumerable<User> user);
        RegisteredUserViewModel UserToRegisteredUserViewModel(IEnumerable<User> user);
        UserViewModel UserToUserViewModel(User user);



    }
}