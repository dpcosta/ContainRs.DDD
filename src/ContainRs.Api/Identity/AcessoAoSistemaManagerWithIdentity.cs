using ContainRs.Api.Data.Migrations;
using ContainRs.Clientes.Cadastro;
using Microsoft.AspNetCore.Identity;
using System.Transactions;

namespace ContainRs.Api.Identity;

internal class AcessoAoSistemaManagerWithIdentity : IAccessoAoSistemaManager
{
    private readonly UserManager<AppUser> _userManager;

    public AcessoAoSistemaManagerWithIdentity(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task AdicionarClienteAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null)
        {
            user = new AppUser
            {
                UserName = email,
                Email = email
            };
            await _userManager.CreateAsync(user, "Alura@123");
            await _userManager.AddToRoleAsync(user, "Cliente");
        }

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
    }

    public async Task<bool?> PossuiAcessoAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return null;
        return user.EmailConfirmed;
    }

    public async Task RejeitaAcessoAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return;

        user.EmailConfirmed = false;
        await _userManager.UpdateAsync(user);
    }

    public async Task RemoveClienteAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user is null) return;
        await _userManager.DeleteAsync(user);
    }
}
