using Gen;
using ManagedServer;
using ManagedServer.Events;
using ManagedServer.Worlds;
using Minecraft.Data.Generated;
using Minecraft.Data.Generated.BlockTypes;
using Minecraft.Schemas.Vec;
using TestApp;


ManagedMinecraftServer server = ManagedMinecraftServer.NewBasic();
GenGame game = new(server, TestConfig.Config);

World world = game.Initialise();
server.Events.AddListener<PlayerPreLoginEvent>(e => {
    e.World = world;
});


world.LoadChunk(new Vec2<int>(0, 0)).Wait();
world.SetBlock((0, 1, 2), Block.NetherPortal with {
    AxisValue = NetherPortalBlock.Axis.Z
});
world.SetBlock((0, 2, 2), Block.NetherPortal with {
    AxisValue = NetherPortalBlock.Axis.Z
});
world.SetBlock((0, 2, 3), Block.NetherPortal with {
    AxisValue = NetherPortalBlock.Axis.Z
});
world.SetBlock((0, 1, 3), Block.NetherPortal with {
    AxisValue = NetherPortalBlock.Axis.Z
});

server.Start();
_ = server.ListenTcp(25565);

Console.WriteLine("Server started.");
server.WaitForExit();
