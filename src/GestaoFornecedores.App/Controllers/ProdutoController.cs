using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestaoFornecedores.Business.Models;
using GestaoFornecedores.Business.Interfaces.Respositories;
using GestaoFornecedores.App.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using GestaoFornecedores.Business.Interfaces.Services;
using GestaoFornecedores.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GestaoFornecedores.App.Extensions;

namespace GestaoFornecedores.App.Controllers
{
    [Authorize]
    public class ProdutoController : BaseController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;

        public ProdutoController(IProdutoRepository produtoRepository,
                                 IFornecedorRepository fornecedorRepository,
                                 IProdutoService produtoService,
                                 INotificador notificador,
                                 IMapper mapper) : base(notificador)
        {
            _produtoRepository = produtoRepository;
            _fornecedorRepository = fornecedorRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [AllowAnonymous]
        [Route("lista-de-produtos")]
        public async Task<IActionResult> Index()
        {
            return View(_mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosForncedores()));
        }

        [AllowAnonymous]
        [Route("dados-de-produtos/{id:guid}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var produtoViewModeel = await ObterProduto(id);
            if (produtoViewModeel == null)
            {
                return NotFound();
            }
            return View(produtoViewModeel);
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("novo-produto")]
        public async Task<IActionResult> Create()
        {
            var produtoViewModel = await PopularFornecedores(new ProdutoViewModel());
            return View(produtoViewModel);
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [Route("novo-produto")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProdutoViewModel produtoViewModel)
        {
            produtoViewModel = await PopularFornecedores(produtoViewModel);
            if (!ModelState.IsValid) return View(produtoViewModel);

            string imgPrefixo = $"{Guid.NewGuid()}_";

            if (!await UpLoadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                return View(produtoViewModel);

            produtoViewModel.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;

            await _produtoService.Adicionar(_mapper.Map<Produto>(produtoViewModel));

            if (!OperacaoValida()) return View(produtoViewModel);

            return RedirectToAction("Index");
        }

        private async Task<bool> UpLoadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }

        [ClaimsAuthorize("Produto", "Editar")]
        [Route("editar-produto/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var produtoViewModeel = await ObterProduto(id);
            if (produtoViewModeel == null)
            {
                return NotFound();
            }
            return View(produtoViewModeel);
        }

        [ClaimsAuthorize("Produto", "Editar")]
        [Route("editar-produto/{id:guid}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ProdutoViewModel produtoViewModel)
        {
            if (id != produtoViewModel.Id) return NotFound();

            var produtoAtualizacao = await ObterProduto(id);
            produtoViewModel.Fornecedor = produtoAtualizacao.Fornecedor;
            produtoViewModel.Imagem = produtoAtualizacao.Imagem;
            if (!ModelState.IsValid) return View(produtoViewModel);

            if(produtoViewModel.ImagemUpload != null)
            {
                string imgPrefixo = $"{Guid.NewGuid()}_";
                if (!await UpLoadArquivo(produtoViewModel.ImagemUpload, imgPrefixo))
                        return View(produtoViewModel);
                produtoAtualizacao.Imagem = imgPrefixo + produtoViewModel.ImagemUpload.FileName;
            }

            produtoAtualizacao.Nome = produtoViewModel.Nome;
            produtoAtualizacao.Descricao = produtoViewModel.Descricao;
            produtoAtualizacao.Valor = produtoViewModel.Valor;
            produtoAtualizacao.Ativo = produtoViewModel.Ativo;

            await _produtoService.Atualizar(_mapper.Map<Produto>(produtoAtualizacao));

            if (!OperacaoValida()) return View(produtoViewModel);

            TempData["Sucesso"] = "Produto excluido com Sucesso!";

            return RedirectToAction("Index");
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [Route("excluir-produto/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var produto = await ObterProduto(id);
            if (produto == null) return NotFound();
            return View(produto);
        }

        [ClaimsAuthorize("Produto", "Excluir")]
        [Route("excluir-produto/{id:guid}")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var produto = await ObterProduto(id);
            if (produto == null) return NotFound();
            await _produtoService.Remover(id);

            if (!OperacaoValida()) return View(produto);
            return RedirectToAction("Index");
        }

        private async Task<ProdutoViewModel> ObterProduto(Guid id)
        {
            var produto = _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterProdutoFornecedor(id));
            produto.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produto;
        }

        private async Task<ProdutoViewModel> PopularFornecedores(ProdutoViewModel produtoVIewModel)
        {
            produtoVIewModel.Fornecedores = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return produtoVIewModel;
        }
    }
}
