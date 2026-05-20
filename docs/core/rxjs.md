# RxJS

## `pipe()`

`pipe()` is a way to chain operations on a stream of values. In other words, each step takes a value, transforms or reacts to it, then passes it to the next step. For example:

```ts
of(1).pipe(
  map(x => x + 1),
  map(x => x * 2)
)
```

The above snippets takes 1, adds 1 to it, then multiplies it by 2.

A pipe will only ever execute once it has been subscribed to.

## `of()`

Creates an observable that immediately emits the value(s) provided, then completes. It is necessary inside `pipe` because everything must return an observable. It's as simple as:

```ts
of(5)
```

Returning:

```ts
5
```

## `tap()`

A way of running code when a value flows through, but without changing the value. Tap is just an operator, and not a method on an observable, so you would still need to use `.pipe()`. `tap()` should only be used for things like:

- Updating a subject
- Logging
- Toggling flags

Example:

```ts
this.loading$ = this.userService.getKids().pipe(
  tap(kids => this.kidsSubject.next(kids))
);
```

The above code will take the output from `getKids` and push it through into the `tap()` and then into `kidsSubject`.

## `shareReplay()`

Can be thought of as:

>"Run once, remember the result, and give the same result to everyone who subscribes later"

Without it, each subscription would mean a new execution. If you were subscribing to some data that was unlikely to change, you may not want to keep refetching it.

If you did:

```ts
const users$ = this.http.get('/api/users').pipe(
  shareReplay(1)
);
```

The `1` specifies the buffer size. The essentially means, how many previous values should be remembered and given to new subscribers. This matters because if a stream is emitting numbers over time, for example:

```text
1 -> 2 -> 3 -> 4
```

With `shareReplay(1)`, a new subscriber that joined at the end of the emission would only receive `4`. The latest value. Whereas with `shareReplay(3)`, the subscriber would receive `2, 3, 4`.

## Example

```ts
vm$!: Observable<{
  kids: User[];
  selectedKid: User | null;
  stats?: KidStats;
}>;

const kids$ = this.userService.getKids();

this.vm$ = kids$.pipe(
  map((kids) => ({
    kids,
    selectedKid: kids[0] || null,
  })),
  switchMap(({ kids, selectedKid }) => {
    if (!selectedKid) {
      return of({
        kids,
        selectedKid: null,
        stats: undefined,
      });
    }

    return this.choreCompletionService.getChoreCompletionStats(selectedKid.id).pipe(
      map((stats) => ({
        kids,
        selectedKid,
        stats,
      })),
    );
  }),
);
```

The above example, gets an observable from `userService`. It then uses `pipe`, to chain other operators together (`map` and `switchMap`). The operator `map` then takes turns `kids` into:

```ts
{
  kids
  selectedKid
}
```

The next section, `switchMap` is required here instead of `map` because when `getChoreCompletionStats` is called, it needs to flatten it out first, then map it otherwise you'd end up with a nested observable.

### Map

```ts
  map((kids) => ({
    kids,
    selectedKid: kids[0] || null,
  })),
```

This is shaping the data into the properties wanted, `kids` and `selectedKid`.

### Switch Map

This means, take a value, and switch to a new observable based on it. This is done in the example because you have the first 2 properties you need from the map, and now you need to create a new one using the `selectedKid` value to get the `stats` property. An example:

```ts
of(1).pipe(
  map(id => http.get(`/user/${id}`))
)
```

becomes:

```ts
Observable<Observable<User>>
```

whereas:

```ts
of(1).pipe(
  switchMap(id => http.get(`/user/${id}`))
)
```

becomes

```ts
Observable<User>
```
