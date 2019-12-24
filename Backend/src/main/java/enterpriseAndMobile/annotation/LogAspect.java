package enterpriseAndMobile.annotation;

import enterpriseAndMobile.jmsQuee.MessageSender;
import org.aspectj.lang.ProceedingJoinPoint;
import org.aspectj.lang.annotation.Around;
import org.aspectj.lang.annotation.Aspect;
import org.springframework.stereotype.Component;

import java.time.LocalDateTime;

@Aspect
@Component
public class LogAspect {

    private final MessageSender messageSender;

    public LogAspect(MessageSender messageSender) {
        this.messageSender = messageSender;
    }

    @Around(value = "@annotation(LogExecutionTime)")
    public Object logExecutionTime(ProceedingJoinPoint joinPoint) throws Throwable {
        long start = System.currentTimeMillis();

        Object proceed = joinPoint.proceed();

        long executionTime = System.currentTimeMillis() - start;

        messageSender.sendMessage("Log message: " + joinPoint.getSignature() + " executed in " + executionTime + "ms at " + LocalDateTime.now());

        return proceed;
    }
}
