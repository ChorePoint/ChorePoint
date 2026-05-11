# ChorePoint Project Structure

The general structure of ChorePoint is feature based. Generally, it will try and follow the smallest scope principle, in which, a type for instance will only be as high in the directory structure as required by the classes using it. The root directory is as follows:

```text
src/
├── consts/
├── core/
├── features/
├── layout/
└── shared/
```

## core

This folder will contain "app-wide singletons" such as guards, interceptors and global services that are only instantiated once.

```text
core/
├── guards/
├── interceptors/
├── services/
└── types/
```

### services

This folder will contain services generally used for global data, such as "who is logged in". The `user-service` for instance is a good example.

```text
services/
├── kids/
└── parents/
```

These inner service files would contain the service itself, and it's related DTOs. For instance:

```text
parents/
├── parents.service.ts
└── parents.dtos.ts
```

In the DTOs file you could expect:

```ts
export type GetKidsResponse = {
  success: boolean;
  message: string;
  data: User[];
};
```

### types

This folder should contain universal blueprints used across the entire application, such as global enums, environment types and shared domain models such as database representations. The structure could look like:

```text
types/
├── models/
├── enums/
├── interfaces/
└── dtos/
```

## shared

This will contain reusable UI such as buttons, cards, common pipes and directives used across multiple modules.

```text
shared/
├── components/
│ 	├── button/ 
│ 	└── card/
├── directive/
└── styles/
  	├── _variables.scss 
  	└── _mixins.scss
```

## features

This will contain independent, self-contained units of business functionality. Each feature can be thought of as a small application for a specific domain. It can look like:

```text
features/
└── products/
    ├── components/         <-- Product-specific UI (e.g., ProductThumbnail)
    ├── pages/              <-- Routed views (e.g., ProductList, ProductDetail)
    ├── services/           <-- API calls just for products
    ├── types/              <-- Product interfaces and DTOs
    └── products.routes.ts  <-- Lazy-loaded route definitions

```

