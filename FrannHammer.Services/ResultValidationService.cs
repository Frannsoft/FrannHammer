using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FrannHammer.Core;
using FrannHammer.Services.Exceptions;

namespace FrannHammer.Services
{
    public interface IResultValidationService
    {
        void ValidateSingleResult<TDto, TPrimaryModelType>(TDto entity, int id = -1);
        void ValidateMultipleResult<TEntity, TDto>(IList<TEntity> entities, int id = -1);

        void ValidateSingleResultFromExpression<TEntity, TDto>(TDto entity,
            Expression<Func<TEntity, bool>> where);

        void ValidateMultipleResultFromExpression<TEntity, TDto>(IList<TDto> entities,
            Expression<Func<TEntity, bool>> where);

        int GetIdFromExpressionBody(Expression body);
    }

    public class ResultValidationService : IResultValidationService
    {
        private const string IdFieldNotIntegerExceptionMessage = "id field is not an integer.  Unable to proceed";

        public void ValidateSingleResult<TDto, TPrimaryModelType>(TDto entity, int id = -1)
        {
            if (entity == null)
            {
                string baseMessage = $"Unable to find any entities of type '{typeof(TPrimaryModelType).Name}' ";
                string extraMessage = id > -1 ? $"with id = {id}" : string.Empty;

                throw new EntityNotFoundException(baseMessage + extraMessage);
            }
        }

        public void ValidateMultipleResult<TEntity, TDto>(IList<TEntity> entities, int id = -1)
        {
            if (entities == null || entities.Count == 0)
            {
                string baseMessage = $"Unable to find any entities of type '{typeof(TDto).Name}' ";
                string extraMessage = id > -1 ? $"with id = {id}" : string.Empty;

                throw new EntityNotFoundException(baseMessage + extraMessage);
            }
        }

        public void ValidateSingleResultFromExpression<TEntity, TDto>(TDto entity,
            Expression<Func<TEntity, bool>> where)
        {
            Guard.VerifyObjectNotNull(entity, nameof(entity));

            var id = GetIdFromExpressionBody(where.Body);

            if (entity == null)
            {
                string baseMessage = $"Unable to find any entities of type '{typeof(TDto).Name}' where character id = {id}";
                throw new EntityNotFoundException(baseMessage);
            }
        }

        public void ValidateMultipleResultFromExpression<TEntity, TDto>(IList<TDto> entities,
            Expression<Func<TEntity, bool>> where)
        {
            Guard.VerifyObjectNotNull(entities, nameof(entities));

            var id = GetIdFromExpressionBody(where.Body);

            if (entities == null || entities.Count == 0)
            {
                string baseMessage = $"Unable to find any entities of type '{typeof(TDto).Name}' where character id = {id}";
                throw new EntityNotFoundException(baseMessage);
            }
        }

        public int GetIdFromExpressionBody(Expression body)
        {
            var binaryExpression = body as BinaryExpression;

            if (binaryExpression == null)
            {
                throw new InvalidCastException(
                    $"body of {nameof(body)} Expression is not a {typeof(BinaryExpression).Name}");
            }

            return GetIdFromExpressionBodyCore(binaryExpression);
        }

        private int GetIdFromExpressionBodyCore(BinaryExpression binaryExpression)
        {
            int idValue;
            if (binaryExpression.Right is MemberExpression)
            {
                var rightMemberExpression = (MemberExpression)binaryExpression.Right;
                idValue = GetIdOfMemberExpression(rightMemberExpression);
            }
            else if (binaryExpression.Right is ConstantExpression)
            {
                var rightConstantExpression = (ConstantExpression)binaryExpression.Right;
                idValue = GetIdOfConstantExpressino(rightConstantExpression);
            }
            else
            { throw new InvalidCastException("Right side expression is not a MemberExpression or ConstantExpression."); }

            return idValue;
        }

        private int GetIdOfMemberExpression(MemberExpression memberExpression)
        {
            int idValue;
            var rightConstantExpression = memberExpression.Expression as ConstantExpression;

            if (rightConstantExpression == null)
            { throw new InvalidCastException($"Right expression Member info is not {typeof(ConstantExpression).Name}. Unable to complete message. "); }

            var constantExpressionValueType = rightConstantExpression.Value.GetType();
            var idFieldInfo = constantExpressionValueType.GetField("id");

            try
            {
                idValue = (int)idFieldInfo.GetValue(rightConstantExpression.Value);
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(IdFieldNotIntegerExceptionMessage);
            }
            return idValue;
        }

        private int GetIdOfConstantExpressino(ConstantExpression constantExpression)
        {
            int idValue;
            try
            {
                idValue = (int)constantExpression.Value;
            }
            catch (InvalidCastException)
            {
                throw new InvalidCastException(IdFieldNotIntegerExceptionMessage);
            }
            return idValue;
        }
    }
}
