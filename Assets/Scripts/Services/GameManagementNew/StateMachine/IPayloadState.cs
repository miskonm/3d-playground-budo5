namespace Playground.Services.GameManagementNew.StateMachine
{
    public interface IPayloadState<in TPayload> : IBaseState
    {
        void Enter(TPayload payload);
    }
}