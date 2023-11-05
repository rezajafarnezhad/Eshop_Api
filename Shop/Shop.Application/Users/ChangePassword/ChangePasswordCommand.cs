using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;

namespace Shop.Application.Users.ChangePassword;

public record ChangePasswordCommand(long UserId, string Pass, string CurrentPass) : IBaseCommand;