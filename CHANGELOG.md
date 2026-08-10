# Changelog

## 1.0.0 (2026-08-10)


### Features

* change container registry to ghcr.io ([458a83d](https://github.com/Carl-Johnsons/tastopia/commit/458a83de91ef595a6811730a03738f67292222e5))
* improve CI pipeline security by offloading parts of it to Argo Workflows ([#264](https://github.com/Carl-Johnsons/tastopia/issues/264)) ([b16c341](https://github.com/Carl-Johnsons/tastopia/commit/b16c341c068a86da158a692bbf97986beb20ffbd))
* support excluding service paths from git log ([e20e06b](https://github.com/Carl-Johnsons/tastopia/commit/e20e06bbd398e50123c1ba4628992cce4555ed9b))


### Bug Fixes

* [#258](https://github.com/Carl-Johnsons/tastopia/issues/258) ([d6af128](https://github.com/Carl-Johnsons/tastopia/commit/d6af128f0f8fb9802edafe572cca13642a3e5819))
* add images to the checklist only on actual updates ([06c5551](https://github.com/Carl-Johnsons/tastopia/commit/06c55517e7f203f71b31430a4bf0e685d70f130a))
* **api:** allow proper header forwarding in staging environment ([f2960a8](https://github.com/Carl-Johnsons/tastopia/commit/f2960a8737404ed1c7b60ad9192f4db009346358))
* avoid redundant ci workflow runs ([9f242e5](https://github.com/Carl-Johnsons/tastopia/commit/9f242e5639c6a317723e974f230bdda3c6f5b33b))
* enhance git bash support on Windows ([747cf0b](https://github.com/Carl-Johnsons/tastopia/commit/747cf0b4c2be071f6906224336846a3962cbff16))
* failed ci condition on non PR events ([707984f](https://github.com/Carl-Johnsons/tastopia/commit/707984f67cc4d7e6b99c876b1a5918db37b08a48))
* limit pulled secrets to the current environment variable ([4d507ea](https://github.com/Carl-Johnsons/tastopia/commit/4d507ea3b0a81481846d08ee12da18cca8804b0e))
* load variable instead of overriding it ([11c9fdf](https://github.com/Carl-Johnsons/tastopia/commit/11c9fdf59b15aa1c6c6753320205657a15543abc))
* make client build image tags unique ([d781fc0](https://github.com/Carl-Johnsons/tastopia/commit/d781fc0a5c023a44a37be7d54880bbc7a15e1442))
* **mobile:** improve cooking mode language selection flow ([7e5f984](https://github.com/Carl-Johnsons/tastopia/commit/7e5f98479b6b33fd27da24947e6729a51115904c))
* offload env provider to CI pipeline ([e42e37d](https://github.com/Carl-Johnsons/tastopia/commit/e42e37d498a1854510e93e4b7530219cb5a90bbd))
* remove redundant service dependencies ([#273](https://github.com/Carl-Johnsons/tastopia/issues/273)) ([2c843e9](https://github.com/Carl-Johnsons/tastopia/commit/2c843e9c829a1e2bb66736d16fc946785a5d04e0))
* use proper group labeling for parallel ci workflows ([e0365c0](https://github.com/Carl-Johnsons/tastopia/commit/e0365c08e4f0fb92bff9a16e56ef71c2d9213861))
* use software rendering backend for ci ([#270](https://github.com/Carl-Johnsons/tastopia/issues/270)) ([ee0499a](https://github.com/Carl-Johnsons/tastopia/commit/ee0499af67d723706f01477fbc22543f4e557536))

## v1.0.0 (2026-05-25)

- Added the initial Tastopia repository.
