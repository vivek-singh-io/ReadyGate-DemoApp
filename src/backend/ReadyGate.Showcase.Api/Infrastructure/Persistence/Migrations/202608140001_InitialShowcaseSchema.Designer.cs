using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ReadyGate.Showcase.Api.Infrastructure.Persistence;

#nullable disable

namespace ReadyGate.Showcase.Api.Infrastructure.Persistence.Migrations;

[DbContext(typeof(ShowcaseDbContext))]
[Migration("202608140001_InitialShowcaseSchema")]
partial class InitialShowcaseSchema
{
    protected override void BuildTargetModel(ModelBuilder modelBuilder) =>
        ShowcaseDbContextModelSnapshot.BuildSnapshotModel(modelBuilder);
}
