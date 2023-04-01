namespace KookBot.Core;

public class InvokeResult {
        public bool Invoked { get; }
        public object? Result { get; }

        protected internal InvokeResult() : this(false, null) { }

        protected internal InvokeResult(bool invoked, object? result) {
                this.Invoked = invoked;
                this.Result = result;
        }
}
