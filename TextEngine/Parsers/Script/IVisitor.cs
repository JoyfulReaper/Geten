using Geten.Parsers.Script.Syntax;

namespace Geten.Parsers.Script
{
    public interface IScriptVisitor
    {
        void Visit(BlockNode block);

        void Visit(AddItemNode node);

        void Visit(CharacterDefinitionNode node);

        void Visit(AskForInputNode node);

        void Visit(CommandNode node);

        void Visit(DecreaseNode node);

        void Visit(IncreaseNode node);

        void Visit(DialogCallNode node);

        void Visit(EventSubscriptionNode node);

        void Visit(ItemDefinitionNode node);

        void Visit(KeyDefinitionNode node);

        void Visit(LiteralNode node);

        void Visit(MemorySlotDefinition node);

        void Visit(PlayNode node);

        void Visit(RemoveItemNode node);

        void Visit(RoomDefinitionNode node);

        void Visit(SetPropertyNode node);

        void Visit(TellNode node);

        void Visit(WeaponDefinitionNode node);

        void Visit(ExitDefinitionNode node);
    }
}