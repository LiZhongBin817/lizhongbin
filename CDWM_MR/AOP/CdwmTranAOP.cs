using Castle.DynamicProxy;
using CDWM_MR.Common;
using CDWM_MR.IRepository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace CDWM_MR.AOP
{
    /// <summary>
    /// 数据库事务切面
    /// </summary>
    public class CdwmTranAOP : IInterceptor
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 构造函数注入
        /// </summary>
        /// <param name="unitOfWork"></param>
        public CdwmTranAOP(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 实例化IInterceptor唯一方法
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            var method = invocation.MethodInvocationTarget ?? invocation.Method;
            //对当前方法的特性验证
            //如果需要验证
            if (method.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(UseTranAttribute)) is UseTranAttribute)
            {
                try
                {
                    Console.WriteLine($"Begin Transaction");

                    _unitOfWork.BeginTran();

                    invocation.Proceed();

                    // 异步获取异常，先执行
                    if (IsAsyncMethod(invocation.Method))
                    {
                        if (invocation.Method.ReturnType == typeof(Task))
                        {
                            invocation.ReturnValue = InternalAsyncHelper.AwaitTaskWithPostActionAndFinally(
                                (Task)invocation.ReturnValue,
                                async () => await TestActionAsync(invocation),
                                ex =>
                                {
                                    _unitOfWork.RollbackTran();

                                });
                        }
                        else //Task<TResult>
                        {
                            invocation.ReturnValue = InternalAsyncHelper.CallAwaitTaskWithPostActionAndFinallyAndGetResult(
                             invocation.Method.ReturnType.GenericTypeArguments[0],
                             invocation.ReturnValue,
                             async () => await TestActionAsync(invocation),
                             ex =>
                             {
                                 _unitOfWork.RollbackTran();

                             });

                        }

                    }
                    _unitOfWork.CommitTran();

                }
                catch (Exception)
                {
                    Console.WriteLine($"Rollback Transaction");
                    _unitOfWork.RollbackTran();
                }
            }
            else
            {
                invocation.Proceed();//直接执行被拦截方法
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        public static bool IsAsyncMethod(MethodInfo method)
        {
            return (
                method.ReturnType == typeof(Task) ||
                (method.ReturnType.IsGenericType && method.ReturnType.GetGenericTypeDefinition() == typeof(Task<>))
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invocation"></param>
        /// <returns></returns>
        private async Task TestActionAsync(IInvocation invocation)
        {
            //await 
        }
    }

}
