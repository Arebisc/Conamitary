import { FooInterface } from './../abstract/fooInterface';
import { ContainerModule, interfaces } from 'inversify';
import { Foo } from '@/services/foo';

export const MainModule = new ContainerModule((bind: interfaces.Bind) => {
    bind(nameof<FooInterface>()).to(Foo);
});
