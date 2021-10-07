﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Vendas.Models
{
    [Table("tb_Venda")]
    public partial class TbVenda
    {
        [Key]
        [Column("Id_Venda")]
        public int IdVenda { get; set; }
        [Column("Id_Cliente")]
        public int IdCliente { get; set; }
        [Column("Id_Produto")]
        public int IdProduto { get; set; }
        [Column("Quantidade_Produto")]
        public int QuantidadeProduto { get; set; }
        [Column("Total_Venda", TypeName = "decimal(18, 0)")]
        public decimal TotalVenda { get; set; }
        [Required]
        [Column("Status_Venda")]
        [StringLength(20)]
        public string StatusVenda { get; set; }
        [Column("Data_Venda", TypeName = "datetime")]
        public DateTime DataVenda { get; set; }

        [ForeignKey(nameof(IdCliente))]
        [InverseProperty(nameof(TbCliente.TbVenda))]
        public virtual TbCliente IdClienteNavigation { get; set; }
        [ForeignKey(nameof(IdProduto))]
        [InverseProperty(nameof(TbProduto.TbVenda))]
        public virtual TbProduto IdProdutoNavigation { get; set; }
    }
}