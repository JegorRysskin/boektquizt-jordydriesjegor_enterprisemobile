package enterpriseAndMobile.util;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;

import java.util.function.Function;
import java.util.function.Supplier;

public final class HttpStatusUtils {
    private HttpStatusUtils() {
    }

    /**
     * Takes an argument and returns an OK {@link ResponseEntity}.
     * @param <T> The type of {@link ResponseEntity} to return.
     * @return A function that takes an argument and returns an OK {@link ResponseEntity}.
     */
    public static <T> Function<T, ResponseEntity<T>> ok() {
        return value -> new ResponseEntity<>(value, HttpStatus.OK);
    }

    /**
     * Returns a Not Found {@link ResponseEntity}.
     * @param <T> The type of {@link ResponseEntity} to return.
     * @return A function that returns a Not Found {@link ResponseEntity}.
     */
    public static <T> Supplier<ResponseEntity<T>> notFound() {
        return () -> new ResponseEntity<>(HttpStatus.NOT_FOUND);
    }

    /**
     * Returns a Bad Request {@link ResponseEntity}.
     * @param <T> The type of {@link ResponseEntity} to return.
     * @return A function that returns a Bad Request {@link ResponseEntity}.
     */
    public static <T> Supplier<ResponseEntity<T>> badRequest() {
        return () -> new ResponseEntity<>(HttpStatus.BAD_REQUEST);
    }
}
